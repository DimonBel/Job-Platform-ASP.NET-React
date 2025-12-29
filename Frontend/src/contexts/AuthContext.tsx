import { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { authApi, UserInfo, AuthResponse } from '@/services/api';

interface AuthContextType {
  user: UserInfo | null;
  token: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (email: string, password: string) => Promise<AuthResponse>;
  register: (data: RegisterData) => Promise<AuthResponse>;
  logout: () => void;
  updateProfile: (data: { firstName: string; lastName: string; phone?: string }) => Promise<UserInfo>;
}

interface RegisterData {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phone?: string;
  companyId?: number;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const TOKEN_KEY = 'career_connect_token';
const USER_KEY = 'career_connect_user';

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<UserInfo | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    // Load stored auth state on mount
    const storedToken = localStorage.getItem(TOKEN_KEY);
    const storedUser = localStorage.getItem(USER_KEY);

    if (storedToken && storedUser) {
      setToken(storedToken);
      setUser(JSON.parse(storedUser));
      
      // Verify token is still valid by fetching current user
      authApi.getCurrentUser(storedToken)
        .then((currentUser) => {
          setUser(currentUser);
          localStorage.setItem(USER_KEY, JSON.stringify(currentUser));
        })
        .catch(() => {
          // Token is invalid, clear auth state
          logout();
        })
        .finally(() => {
          setIsLoading(false);
        });
    } else {
      setIsLoading(false);
    }
  }, []);

  const login = async (email: string, password: string): Promise<AuthResponse> => {
    const response = await authApi.login({ email, password });
    
    if (response.success && response.token && response.user) {
      setToken(response.token);
      setUser(response.user);
      localStorage.setItem(TOKEN_KEY, response.token);
      localStorage.setItem(USER_KEY, JSON.stringify(response.user));
    }
    
    return response;
  };

  const register = async (data: RegisterData): Promise<AuthResponse> => {
    const response = await authApi.register(data);
    
    if (response.success && response.token && response.user) {
      setToken(response.token);
      setUser(response.user);
      localStorage.setItem(TOKEN_KEY, response.token);
      localStorage.setItem(USER_KEY, JSON.stringify(response.user));
    }
    
    return response;
  };

  const logout = () => {
    setToken(null);
    setUser(null);
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
  };

  const updateProfile = async (data: { firstName: string; lastName: string; phone?: string }): Promise<UserInfo> => {
    if (!token) {
      throw new Error('Not authenticated');
    }
    
    const updatedUser = await authApi.updateProfile(token, data);
    setUser(updatedUser);
    localStorage.setItem(USER_KEY, JSON.stringify(updatedUser));
    return updatedUser;
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        isAuthenticated: !!token && !!user,
        isLoading,
        login,
        register,
        logout,
        updateProfile,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}

// Helper to get auth token for API calls
export function getAuthToken(): string | null {
  return localStorage.getItem(TOKEN_KEY);
}
