import { Link } from "react-router-dom";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import SearchBar from "@/components/SearchBar";
import JobCard from "@/components/JobCard";
import CategoryCard from "@/components/CategoryCard";
import StatCard from "@/components/StatCard";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Skeleton } from "@/components/ui/skeleton";
import { useFeaturedJobs, useCategories, useStatistics } from "@/services/hooks";
import {
  Code2,
  Palette,
  TrendingUp,
  Megaphone,
  Stethoscope,
  GraduationCap,
  Building2,
  Truck,
  ArrowRight,
  Briefcase,
  Users,
  Globe,
  CheckCircle2,
  Sparkles,
  type LucideIcon,
} from "lucide-react";

// Icon mapping for categories from the backend
const iconMap: Record<string, LucideIcon> = {
  Code2: Code2,
  Palette: Palette,
  TrendingUp: TrendingUp,
  Megaphone: Megaphone,
  Stethoscope: Stethoscope,
  GraduationCap: GraduationCap,
  Building2: Building2,
  Truck: Truck,
};

// Default color mapping for categories
const defaultColors: Record<string, string> = {
  Technology: "bg-blue-500/10 text-blue-600",
  Design: "bg-pink-500/10 text-pink-600",
  Finance: "bg-green-500/10 text-green-600",
  Marketing: "bg-orange-500/10 text-orange-600",
  Healthcare: "bg-red-500/10 text-red-600",
  Education: "bg-purple-500/10 text-purple-600",
  "Real Estate": "bg-teal-500/10 text-teal-600",
  Logistics: "bg-amber-500/10 text-amber-600",
};

const formatNumber = (num: number): string => {
  if (num >= 1000000) return `${(num / 1000000).toFixed(0)}M+`;
  if (num >= 1000) return `${(num / 1000).toFixed(0)}K+`;
  return `${num}+`;
};

const Index = () => {
  const { data: featuredJobs, isLoading: jobsLoading } = useFeaturedJobs(4);
  const { data: categories, isLoading: categoriesLoading } = useCategories();
  const { data: statistics, isLoading: statsLoading } = useStatistics();

  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />

      <main className="flex-1">
        {/* Hero Section */}
        <section className="relative overflow-hidden bg-primary py-20 md:py-28">
          {/* Background Pattern */}
          <div className="absolute inset-0 opacity-10">
            <div className="absolute top-0 left-0 w-96 h-96 bg-accent rounded-full blur-3xl -translate-x-1/2 -translate-y-1/2" />
            <div className="absolute bottom-0 right-0 w-96 h-96 bg-accent rounded-full blur-3xl translate-x-1/2 translate-y-1/2" />
          </div>

          <div className="container mx-auto px-4 relative z-10">
            <div className="text-center max-w-4xl mx-auto mb-10">
              <Badge variant="secondary" className="mb-6 animate-fade-in">
                <Sparkles className="h-3 w-3 mr-1" />
                Over {statistics ? formatNumber(statistics.activeJobs) : '50,000+'} jobs available
              </Badge>
              <h1 className="text-4xl md:text-5xl lg:text-6xl font-bold text-primary-foreground mb-6 animate-fade-in stagger-1 text-balance">
                Find Your Dream Job{" "}
                <span className="text-accent">Today</span>
              </h1>
              <p className="text-lg md:text-xl text-primary-foreground/70 mb-10 animate-fade-in stagger-2 max-w-2xl mx-auto">
                Connect with top companies and discover opportunities that match your skills and aspirations. Your next career move starts here.
              </p>
            </div>

            <div className="animate-fade-in stagger-3">
              <SearchBar />
            </div>

            <div className="flex flex-wrap justify-center gap-4 mt-8 text-sm text-primary-foreground/60 animate-fade-in stagger-4">
              <span className="font-medium">Popular:</span>
              {["Remote", "Software Engineer", "Product Manager", "Designer", "Marketing"].map((term) => (
                <Link
                  key={term}
                  to={`/jobs?q=${encodeURIComponent(term)}`}
                  className="hover:text-accent transition-colors"
                >
                  {term}
                </Link>
              ))}
            </div>

            {/* Stats */}
            <div className="grid grid-cols-2 md:grid-cols-4 gap-8 mt-16 pt-16 border-t border-primary-foreground/10 animate-fade-in stagger-5">
              {statsLoading ? (
                <>
                  {[1, 2, 3, 4].map((i) => (
                    <div key={i} className="text-center">
                      <Skeleton className="h-8 w-20 mx-auto mb-2 bg-primary-foreground/10" />
                      <Skeleton className="h-4 w-24 mx-auto bg-primary-foreground/10" />
                    </div>
                  ))}
                </>
              ) : (
                <>
                  <StatCard value={formatNumber(statistics?.activeJobs || 50000)} label="Active Jobs" icon={<Briefcase className="h-6 w-6 text-accent" />} />
                  <StatCard value={formatNumber(statistics?.companies || 12000)} label="Companies" icon={<Building2 className="h-6 w-6 text-accent" />} />
                  <StatCard value={formatNumber(statistics?.jobSeekers || 3000000)} label="Job Seekers" icon={<Users className="h-6 w-6 text-accent" />} />
                  <StatCard value={formatNumber(statistics?.countries || 150)} label="Countries" icon={<Globe className="h-6 w-6 text-accent" />} />
                </>
              )}
            </div>
          </div>
        </section>

        {/* Categories Section */}
        <section className="py-16 md:py-24">
          <div className="container mx-auto px-4">
            <div className="text-center mb-12">
              <h2 className="text-3xl md:text-4xl font-bold text-foreground mb-4">
                Explore by Category
              </h2>
              <p className="text-lg text-muted-foreground max-w-2xl mx-auto">
                Browse through various industries and find the perfect role for you
              </p>
            </div>

            <div className="grid grid-cols-2 md:grid-cols-4 gap-4 md:gap-6">
              {categoriesLoading ? (
                <>
                  {[1, 2, 3, 4, 5, 6, 7, 8].map((i) => (
                    <div key={i} className="p-6 rounded-xl border border-border/50">
                      <Skeleton className="h-12 w-12 rounded-lg mb-4" />
                      <Skeleton className="h-5 w-24 mb-2" />
                      <Skeleton className="h-4 w-16" />
                    </div>
                  ))}
                </>
              ) : (
                categories?.map((category, index) => {
                  const IconComponent = iconMap[category.icon || ''] || Briefcase;
                  const color = category.color || defaultColors[category.title] || "bg-gray-500/10 text-gray-600";
                  
                  return (
                    <div
                      key={category.id}
                      className="animate-fade-in"
                      style={{ animationDelay: `${index * 0.1}s` }}
                    >
                      <CategoryCard 
                        icon={IconComponent} 
                        title={category.title} 
                        count={category.count} 
                        color={color} 
                      />
                    </div>
                  );
                })
              )}
            </div>
          </div>
        </section>

        {/* Featured Jobs Section */}
        <section className="py-16 md:py-24 bg-muted/30">
          <div className="container mx-auto px-4">
            <div className="flex flex-col md:flex-row md:items-end justify-between gap-4 mb-12">
              <div>
                <h2 className="text-3xl md:text-4xl font-bold text-foreground mb-4">
                  Featured Jobs
                </h2>
                <p className="text-lg text-muted-foreground">
                  Hand-picked opportunities from top companies
                </p>
              </div>
              <Button variant="outline" asChild>
                <Link to="/jobs">
                  View All Jobs
                  <ArrowRight className="h-4 w-4 ml-2" />
                </Link>
              </Button>
            </div>

            <div className="grid md:grid-cols-2 gap-6">
              {jobsLoading ? (
                <>
                  {[1, 2, 3, 4].map((i) => (
                    <div key={i} className="p-6 rounded-2xl border border-border/50 bg-card">
                      <div className="flex gap-4">
                        <Skeleton className="h-14 w-14 rounded-xl" />
                        <div className="flex-1">
                          <Skeleton className="h-6 w-48 mb-2" />
                          <Skeleton className="h-4 w-32 mb-4" />
                          <div className="flex gap-4">
                            <Skeleton className="h-4 w-24" />
                            <Skeleton className="h-4 w-24" />
                          </div>
                        </div>
                      </div>
                    </div>
                  ))}
                </>
              ) : (
                featuredJobs?.map((job, index) => (
                  <div
                    key={job.id}
                    className="animate-fade-in"
                    style={{ animationDelay: `${index * 0.1}s` }}
                  >
                    <JobCard job={{
                      id: String(job.id),
                      title: job.title,
                      company: job.company,
                      companyLogo: job.companyLogo,
                      location: job.location,
                      type: job.type as "Full-time" | "Part-time" | "Contract" | "Remote" | "Hybrid",
                      salary: job.salary,
                      postedAt: job.postedAt,
                      tags: job.tags,
                      featured: job.featured,
                    }} />
                  </div>
                ))
              )}
            </div>
          </div>
        </section>

        {/* Why Choose Us Section */}
        <section className="py-16 md:py-24">
          <div className="container mx-auto px-4">
            <div className="grid lg:grid-cols-2 gap-12 items-center">
              <div>
                <Badge variant="accent" className="mb-6">
                  Why JobFlow?
                </Badge>
                <h2 className="text-3xl md:text-4xl font-bold text-foreground mb-6">
                  The Smarter Way to Find Your Next Opportunity
                </h2>
                <p className="text-lg text-muted-foreground mb-8">
                  We're not just another job board. We're your career partner, helping you find roles that truly match your skills and aspirations.
                </p>

                <div className="space-y-4">
                  {[
                    "AI-powered job matching",
                    "Real-time application tracking",
                    "Direct messaging with recruiters",
                    "Salary insights and comparisons",
                    "Career growth resources",
                  ].map((feature) => (
                    <div key={feature} className="flex items-center gap-3">
                      <CheckCircle2 className="h-5 w-5 text-accent shrink-0" />
                      <span className="text-foreground">{feature}</span>
                    </div>
                  ))}
                </div>

                <div className="flex flex-wrap gap-4 mt-8">
                  <Button variant="accent" size="lg" asChild>
                    <Link to="/signup">Get Started Free</Link>
                  </Button>
                  <Button variant="outline" size="lg" asChild>
                    <Link to="/about">Learn More</Link>
                  </Button>
                </div>
              </div>

              <div className="relative">
                <div className="aspect-square rounded-3xl bg-gradient-to-br from-accent/20 to-primary/10 p-8 flex items-center justify-center">
                  <div className="w-full max-w-sm space-y-4">
                    {/* Mock Dashboard Preview */}
                    <div className="bg-card rounded-xl p-4 shadow-lg border border-border/50 animate-float">
                      <div className="flex items-center gap-3 mb-3">
                        <div className="h-10 w-10 rounded-lg bg-accent/10 flex items-center justify-center">
                          <Briefcase className="h-5 w-5 text-accent" />
                        </div>
                        <div>
                          <div className="h-3 w-24 bg-muted rounded" />
                          <div className="h-2 w-16 bg-muted rounded mt-1.5" />
                        </div>
                      </div>
                      <div className="space-y-2">
                        <div className="h-2 w-full bg-muted rounded" />
                        <div className="h-2 w-3/4 bg-muted rounded" />
                      </div>
                    </div>
                    <div className="bg-card rounded-xl p-4 shadow-lg border border-border/50 ml-8 animate-float" style={{ animationDelay: "0.5s" }}>
                      <div className="flex items-center gap-2 mb-2">
                        <CheckCircle2 className="h-5 w-5 text-accent" />
                        <span className="text-sm font-medium text-foreground">Application Sent!</span>
                      </div>
                      <p className="text-xs text-muted-foreground">Your resume is being reviewed</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>

        {/* CTA Section */}
        <section className="py-16 md:py-24 bg-primary">
          <div className="container mx-auto px-4 text-center">
            <h2 className="text-3xl md:text-4xl font-bold text-primary-foreground mb-6">
              Ready to Take the Next Step?
            </h2>
            <p className="text-lg text-primary-foreground/70 mb-8 max-w-2xl mx-auto">
              Join millions of professionals who have found their dream jobs through JobFlow. Your success story could be next.
            </p>
            <div className="flex flex-wrap justify-center gap-4">
              <Button variant="hero" size="xl" asChild>
                <Link to="/signup">
                  Create Free Account
                  <ArrowRight className="h-5 w-5 ml-2" />
                </Link>
              </Button>
              <Button variant="hero-outline" size="xl" asChild>
                <Link to="/jobs">Browse Jobs</Link>
              </Button>
            </div>
          </div>
        </section>
      </main>

      <Footer />
    </div>
  );
};

export default Index;
