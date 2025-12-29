// API Configuration
const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

// Auth Types
export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phone?: string;
  companyId?: number;
}

export interface UserInfo {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  fullName: string;
  role: string;
  profilePicture?: string;
  companyId?: number;
}

export interface AuthResponse {
  success: boolean;
  token?: string;
  expiresAt?: string;
  user?: UserInfo;
  error?: string;
}

// Types matching the backend DTOs
export interface Job {
  id: number;
  title: string;
  company: string;
  companyLogo?: string;
  companyId: number;
  location: string;
  type: string;
  salary: string;
  postedAt: string;
  tags: string[];
  featured: boolean;
}

export interface JobDetail extends Job {
  description?: string;
  responsibilities: string[];
  requirements: string[];
  benefits: string[];
  companyDescription?: string;
  companySize?: string;
  companyWebsite?: string;
  applicants: number;
  category?: string;
}

export interface Company {
  id: number;
  name: string;
  industry?: string;
  location?: string;
  size?: string;
  description?: string;
  logo?: string;
  website?: string;
  rating?: number;
  reviewCount: number;
  openJobsCount: number;
  isVerified?: boolean;
}

export interface CompanyDetail extends Company {
  foundedYear?: string;
  recentJobs: Job[];
}

export interface Category {
  id: number;
  title: string;
  icon?: string;
  color?: string;
  count: number;
}

export interface Statistics {
  activeJobs: number;
  companies: number;
  jobSeekers: number;
  countries: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface JobSearchParams {
  query?: string;
  location?: string;
  locations?: string[];
  jobType?: string;
  jobTypes?: string[];
  experienceLevels?: string[];
  categoryId?: number;
  companyId?: number;
  page?: number;
  pageSize?: number;
}

export interface CompanySearchParams {
  query?: string;
  industry?: string;
  location?: string;
  page?: number;
  pageSize?: number;
}

// Helper function for API calls
async function fetchApi<T>(endpoint: string, options?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${endpoint}`, {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...options?.headers,
    },
  });

  if (!response.ok) {
    const error = await response.json().catch(() => ({ message: 'An error occurred' }));
    throw new Error(error.message || `HTTP error! status: ${response.status}`);
  }

  return response.json();
}

// Jobs API
export const jobsApi = {
  getAll: () => fetchApi<Job[]>('/jobs'),
  
  getFeatured: (count = 4) => fetchApi<Job[]>(`/jobs/featured?count=${count}`),
  
  getById: (id: number | string) => fetchApi<Job>(`/jobs/${id}`),
  
  getDetails: (id: number | string) => fetchApi<JobDetail>(`/jobs/${id}`),
  
  search: (params: JobSearchParams) => {
    const searchParams = new URLSearchParams();
    
    if (params.query) searchParams.append('query', params.query);
    if (params.location) searchParams.append('location', params.location);
    if (params.jobType) searchParams.append('jobType', params.jobType);
    if (params.categoryId) searchParams.append('categoryId', params.categoryId.toString());
    if (params.companyId) searchParams.append('companyId', params.companyId.toString());
    if (params.page) searchParams.append('page', params.page.toString());
    if (params.pageSize) searchParams.append('pageSize', params.pageSize.toString());
    
    // Handle array parameters
    if (params.jobTypes && params.jobTypes.length > 0) {
      params.jobTypes.forEach(type => searchParams.append('jobTypes', type));
    }
    if (params.experienceLevels && params.experienceLevels.length > 0) {
      params.experienceLevels.forEach(level => searchParams.append('experienceLevels', level));
    }
    if (params.locations && params.locations.length > 0) {
      params.locations.forEach(loc => searchParams.append('locations', loc));
    }
    
    return fetchApi<PagedResult<Job>>(`/jobs?${searchParams.toString()}`);
  },
};

// Companies API
export const companiesApi = {
  getAll: () => fetchApi<Company[]>('/companies'),
  
  search: (params: CompanySearchParams) => {
    const searchParams = new URLSearchParams();
    
    if (params.query) searchParams.append('query', params.query);
    if (params.industry) searchParams.append('industry', params.industry);
    if (params.location) searchParams.append('location', params.location);
    if (params.page) searchParams.append('page', params.page.toString());
    if (params.pageSize) searchParams.append('pageSize', params.pageSize.toString());
    
    return fetchApi<PagedResult<Company>>(`/companies?${searchParams.toString()}`);
  },
  
  getTop: (count = 6) => fetchApi<Company[]>(`/companies/top?count=${count}`),
  
  getById: (id: number | string) => fetchApi<CompanyDetail>(`/companies/${id}`),
  
  getDetails: (id: number | string) => fetchApi<CompanyDetail>(`/companies/${id}`),
};

// Categories API
export const categoriesApi = {
  getAll: () => fetchApi<Category[]>('/categories'),
  
  getById: (id: number | string) => fetchApi<Category>(`/categories/${id}`),
};

// Statistics API
export const statisticsApi = {
  get: () => fetchApi<Statistics>('/statistics'),
};

// Tags API
export const tagsApi = {
  getAll: () => fetchApi<string[]>('/tags'),
};

// Auth API
export const authApi = {
  login: async (dto: LoginDto): Promise<AuthResponse> => {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto),
      });
      
      const data = await response.json();
      
      if (!response.ok) {
        return { success: false, error: data.error || 'Login failed' };
      }
      
      return data;
    } catch {
      return { success: false, error: 'Network error. Please try again.' };
    }
  },
  
  register: async (dto: RegisterDto): Promise<AuthResponse> => {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto),
      });
      
      const data = await response.json();
      
      if (!response.ok) {
        return { success: false, error: data.error || 'Registration failed' };
      }
      
      return data;
    } catch {
      return { success: false, error: 'Network error. Please try again.' };
    }
  },
  
  getCurrentUser: async (token: string): Promise<UserInfo> => {
    const response = await fetch(`${API_BASE_URL}/auth/me`, {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
    });
    
    if (!response.ok) {
      throw new Error('Failed to get current user');
    }
    
    return response.json();
  },
  
  refreshToken: async (token: string): Promise<AuthResponse> => {
    const response = await fetch(`${API_BASE_URL}/auth/refresh`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
    });
    
    if (!response.ok) {
      throw new Error('Failed to refresh token');
    }
    
    return response.json();
  },
  
  updateProfile: async (token: string, data: { firstName: string; lastName: string; phone?: string }): Promise<UserInfo> => {
    const response = await fetch(`${API_BASE_URL}/auth/profile`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
      body: JSON.stringify(data),
    });
    
    if (!response.ok) {
      const error = await response.json().catch(() => ({ error: 'Failed to update profile' }));
      throw new Error(error.error || 'Failed to update profile');
    }
    
    return response.json();
  },
};
