import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "@/contexts/AuthContext";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Badge } from "@/components/ui/badge";
import { Skeleton } from "@/components/ui/skeleton";
import { Alert, AlertDescription } from "@/components/ui/alert";
import {
  User,
  Mail,
  Phone,
  Building2,
  Calendar,
  Edit2,
  LogOut,
  Shield,
  Briefcase,
  Loader2,
  CheckCircle2,
} from "lucide-react";

const Profile = () => {
  const navigate = useNavigate();
  const { user, isAuthenticated, isLoading, logout, updateProfile } = useAuth();
  const [isEditing, setIsEditing] = useState(false);
  const [isSaving, setIsSaving] = useState(false);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  
  // Form state
  const [formData, setFormData] = useState({
    firstName: user?.firstName || "",
    lastName: user?.lastName || "",
    phone: "",
  });

  // Redirect to login if not authenticated
  if (!isLoading && !isAuthenticated) {
    navigate("/login", { state: { from: "/profile" } });
    return null;
  }

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  const handleEditClick = () => {
    setFormData({
      firstName: user?.firstName || "",
      lastName: user?.lastName || "",
      phone: "",
    });
    setError("");
    setSuccess("");
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setError("");
    setSuccess("");
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setSuccess("");

    if (!formData.firstName.trim() || !formData.lastName.trim()) {
      setError("First name and last name are required");
      return;
    }

    setIsSaving(true);

    try {
      await updateProfile({
        firstName: formData.firstName.trim(),
        lastName: formData.lastName.trim(),
        phone: formData.phone.trim() || undefined,
      });
      setSuccess("Profile updated successfully!");
      setIsEditing(false);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to update profile");
    } finally {
      setIsSaving(false);
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen flex flex-col bg-background">
        <Navbar />
        <main className="flex-1">
          <section className="bg-muted/30 py-12 border-b border-border/40">
            <div className="container mx-auto px-4">
              <div className="flex flex-col md:flex-row gap-6 items-start">
                <Skeleton className="h-32 w-32 rounded-full" />
                <div className="space-y-4 flex-1">
                  <Skeleton className="h-8 w-48" />
                  <Skeleton className="h-5 w-32" />
                  <Skeleton className="h-5 w-64" />
                </div>
              </div>
            </div>
          </section>
          <section className="py-8">
            <div className="container mx-auto px-4">
              <div className="grid md:grid-cols-2 gap-6">
                <Skeleton className="h-64 rounded-2xl" />
                <Skeleton className="h-64 rounded-2xl" />
              </div>
            </div>
          </section>
        </main>
        <Footer />
      </div>
    );
  }

  const getRoleBadgeVariant = (role: string) => {
    switch (role) {
      case "Admin":
        return "destructive";
      case "Employer":
        return "accent";
      default:
        return "secondary";
    }
  };

  const getRoleIcon = (role: string) => {
    switch (role) {
      case "Admin":
        return Shield;
      case "Employer":
        return Building2;
      default:
        return Briefcase;
    }
  };

  const RoleIcon = getRoleIcon(user?.role || "JobSeeker");

  return (
    <div className="min-h-screen flex flex-col bg-background">
      <Navbar />

      <main className="flex-1">
        {/* Profile Header */}
        <section className="bg-muted/30 py-12 border-b border-border/40">
          <div className="container mx-auto px-4">
            <div className="flex flex-col md:flex-row gap-6 items-start md:items-center">
              {/* Avatar */}
              <div className="relative">
                <div className="h-32 w-32 rounded-full bg-accent/10 border-4 border-background shadow-lg flex items-center justify-center overflow-hidden">
                  {user?.profilePicture ? (
                    <img
                      src={user.profilePicture}
                      alt={user.fullName}
                      className="h-full w-full object-cover"
                    />
                  ) : (
                    <span className="text-4xl font-bold text-accent">
                      {user?.firstName?.charAt(0)}
                      {user?.lastName?.charAt(0)}
                    </span>
                  )}
                </div>
              </div>

              {/* User Info */}
              <div className="flex-1">
                <div className="flex flex-wrap items-center gap-3 mb-2">
                  <h1 className="text-2xl md:text-3xl font-bold text-foreground">
                    {user?.fullName}
                  </h1>
                  <Badge variant={getRoleBadgeVariant(user?.role || "JobSeeker")}>
                    <RoleIcon className="h-3 w-3 mr-1" />
                    {user?.role}
                  </Badge>
                </div>
                <p className="text-muted-foreground flex items-center gap-2 mb-4">
                  <Mail className="h-4 w-4" />
                  {user?.email}
                </p>
                <div className="flex flex-wrap gap-3">
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={isEditing ? handleCancelEdit : handleEditClick}
                  >
                    <Edit2 className="h-4 w-4 mr-2" />
                    {isEditing ? "Cancel" : "Edit Profile"}
                  </Button>
                  <Button
                    variant="ghost"
                    size="sm"
                    onClick={handleLogout}
                    className="text-destructive hover:text-destructive hover:bg-destructive/10"
                  >
                    <LogOut className="h-4 w-4 mr-2" />
                    Sign Out
                  </Button>
                </div>
              </div>
            </div>
          </div>
        </section>

        {/* Profile Content */}
        <section className="py-8">
          <div className="container mx-auto px-4">
            <div className="grid md:grid-cols-2 gap-6">
              {/* Personal Information */}
              <div className="bg-card rounded-2xl border border-border/50 p-6">
                <h2 className="text-lg font-semibold text-foreground mb-6 flex items-center gap-2">
                  <User className="h-5 w-5 text-accent" />
                  Personal Information
                </h2>

                {error && (
                  <Alert variant="destructive" className="mb-4">
                    <AlertDescription>{error}</AlertDescription>
                  </Alert>
                )}

                {success && (
                  <Alert className="mb-4 border-green-500 bg-green-50 text-green-700">
                    <CheckCircle2 className="h-4 w-4" />
                    <AlertDescription>{success}</AlertDescription>
                  </Alert>
                )}

                {isEditing ? (
                  <form onSubmit={handleSubmit} className="space-y-4">
                    <div className="grid grid-cols-2 gap-4">
                      <div className="space-y-2">
                        <Label htmlFor="firstName">First Name</Label>
                        <Input
                          id="firstName"
                          name="firstName"
                          value={formData.firstName}
                          onChange={handleChange}
                          placeholder="First name"
                          disabled={isSaving}
                        />
                      </div>
                      <div className="space-y-2">
                        <Label htmlFor="lastName">Last Name</Label>
                        <Input
                          id="lastName"
                          name="lastName"
                          value={formData.lastName}
                          onChange={handleChange}
                          placeholder="Last name"
                          disabled={isSaving}
                        />
                      </div>
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="email">Email</Label>
                      <Input
                        id="email"
                        type="email"
                        defaultValue={user?.email}
                        placeholder="Email address"
                        disabled
                      />
                      <p className="text-xs text-muted-foreground">
                        Email cannot be changed
                      </p>
                    </div>
                    <div className="space-y-2">
                      <Label htmlFor="phone">Phone</Label>
                      <Input
                        id="phone"
                        name="phone"
                        type="tel"
                        value={formData.phone}
                        onChange={handleChange}
                        placeholder="Phone number"
                        disabled={isSaving}
                      />
                    </div>
                    <Button type="submit" className="w-full" disabled={isSaving}>
                      {isSaving ? (
                        <>
                          <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                          Saving...
                        </>
                      ) : (
                        "Save Changes"
                      )}
                    </Button>
                  </form>
                ) : (
                  <div className="space-y-4">
                    <div className="flex items-center gap-3 p-3 rounded-lg bg-muted/50">
                      <User className="h-5 w-5 text-muted-foreground" />
                      <div>
                        <p className="text-sm text-muted-foreground">Full Name</p>
                        <p className="font-medium text-foreground">
                          {user?.fullName}
                        </p>
                      </div>
                    </div>
                    <div className="flex items-center gap-3 p-3 rounded-lg bg-muted/50">
                      <Mail className="h-5 w-5 text-muted-foreground" />
                      <div>
                        <p className="text-sm text-muted-foreground">Email</p>
                        <p className="font-medium text-foreground">
                          {user?.email}
                        </p>
                      </div>
                    </div>
                    <div className="flex items-center gap-3 p-3 rounded-lg bg-muted/50">
                      <Phone className="h-5 w-5 text-muted-foreground" />
                      <div>
                        <p className="text-sm text-muted-foreground">Phone</p>
                        <p className="font-medium text-foreground">
                          Not provided
                        </p>
                      </div>
                    </div>
                  </div>
                )}
              </div>

              {/* Account Information */}
              <div className="bg-card rounded-2xl border border-border/50 p-6">
                <h2 className="text-lg font-semibold text-foreground mb-6 flex items-center gap-2">
                  <Shield className="h-5 w-5 text-accent" />
                  Account Information
                </h2>

                <div className="space-y-4">
                  <div className="flex items-center gap-3 p-3 rounded-lg bg-muted/50">
                    <RoleIcon className="h-5 w-5 text-muted-foreground" />
                    <div>
                      <p className="text-sm text-muted-foreground">Account Type</p>
                      <p className="font-medium text-foreground">{user?.role}</p>
                    </div>
                  </div>
                  <div className="flex items-center gap-3 p-3 rounded-lg bg-muted/50">
                    <Calendar className="h-5 w-5 text-muted-foreground" />
                    <div>
                      <p className="text-sm text-muted-foreground">Member Since</p>
                      <p className="font-medium text-foreground">
                        December 2024
                      </p>
                    </div>
                  </div>
                  {user?.companyId && (
                    <div className="flex items-center gap-3 p-3 rounded-lg bg-muted/50">
                      <Building2 className="h-5 w-5 text-muted-foreground" />
                      <div>
                        <p className="text-sm text-muted-foreground">Company ID</p>
                        <p className="font-medium text-foreground">
                          {user.companyId}
                        </p>
                      </div>
                    </div>
                  )}
                </div>

                <div className="mt-6 pt-6 border-t border-border/50">
                  <h3 className="text-sm font-medium text-foreground mb-4">
                    Quick Actions
                  </h3>
                  <div className="space-y-2">
                    {user?.role === "JobSeeker" && (
                      <>
                        <Button
                          variant="outline"
                          className="w-full justify-start"
                          onClick={() => navigate("/jobs")}
                        >
                          <Briefcase className="h-4 w-4 mr-2" />
                          Browse Jobs
                        </Button>
                        <Button
                          variant="outline"
                          className="w-full justify-start"
                          onClick={() => navigate("/companies")}
                        >
                          <Building2 className="h-4 w-4 mr-2" />
                          View Companies
                        </Button>
                      </>
                    )}
                    {user?.role === "Employer" && (
                      <Button
                        variant="outline"
                        className="w-full justify-start"
                        onClick={() => navigate(`/companies/${user.companyId}`)}
                      >
                        <Building2 className="h-4 w-4 mr-2" />
                        View My Company
                      </Button>
                    )}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
      </main>

      <Footer />
    </div>
  );
};

export default Profile;
