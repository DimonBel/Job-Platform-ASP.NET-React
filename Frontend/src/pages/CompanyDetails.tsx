import { useParams, Link } from "react-router-dom";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Skeleton } from "@/components/ui/skeleton";
import { useCompanyDetails } from "@/services/hooks";
import {
  MapPin,
  Building2,
  Users,
  Briefcase,
  Star,
  ArrowLeft,
  ExternalLink,
  Globe,
  Calendar,
  CheckCircle2,
  Clock,
  DollarSign,
} from "lucide-react";

const CompanyDetails = () => {
  const { id } = useParams();
  const { data: company, isLoading, error } = useCompanyDetails(id || "");

  if (isLoading) {
    return (
      <div className="min-h-screen flex flex-col bg-background">
        <Navbar />
        <main className="flex-1">
          <section className="bg-muted/30 py-8 border-b border-border/40">
            <div className="container mx-auto px-4">
              <Skeleton className="h-5 w-32 mb-6" />
              <div className="flex flex-col lg:flex-row gap-6 lg:items-start">
                <Skeleton className="h-24 w-24 rounded-2xl" />
                <div className="flex-1 space-y-4">
                  <Skeleton className="h-8 w-64" />
                  <Skeleton className="h-5 w-40" />
                  <div className="flex gap-4">
                    <Skeleton className="h-5 w-32" />
                    <Skeleton className="h-5 w-32" />
                    <Skeleton className="h-5 w-32" />
                  </div>
                </div>
              </div>
            </div>
          </section>
          <section className="py-8">
            <div className="container mx-auto px-4">
              <div className="flex flex-col lg:flex-row gap-8">
                <div className="flex-1 space-y-8">
                  <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                    <Skeleton className="h-6 w-48 mb-4" />
                    <div className="space-y-3">
                      <Skeleton className="h-4 w-full" />
                      <Skeleton className="h-4 w-3/4" />
                      <Skeleton className="h-4 w-5/6" />
                    </div>
                  </div>
                  <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                    <Skeleton className="h-6 w-48 mb-4" />
                    <div className="space-y-4">
                      {Array.from({ length: 3 }).map((_, i) => (
                        <Skeleton key={i} className="h-24 w-full rounded-xl" />
                      ))}
                    </div>
                  </div>
                </div>
                <aside className="lg:w-80 shrink-0 space-y-6">
                  <Skeleton className="h-48 w-full rounded-2xl" />
                </aside>
              </div>
            </div>
          </section>
        </main>
        <Footer />
      </div>
    );
  }

  if (error || !company) {
    return (
      <div className="min-h-screen flex flex-col">
        <Navbar />
        <main className="flex-1 flex items-center justify-center">
          <div className="text-center">
            <div className="w-16 h-16 mx-auto mb-4 rounded-full bg-muted flex items-center justify-center">
              <Building2 className="h-8 w-8 text-muted-foreground" />
            </div>
            <h1 className="text-2xl font-bold text-foreground mb-4">Company not found</h1>
            <p className="text-muted-foreground mb-6">
              The company you're looking for doesn't exist or has been removed.
            </p>
            <Button asChild>
              <Link to="/companies">Back to Companies</Link>
            </Button>
          </div>
        </main>
        <Footer />
      </div>
    );
  }

  return (
    <div className="min-h-screen flex flex-col bg-background">
      <Navbar />

      <main className="flex-1">
        {/* Header */}
        <section className="bg-muted/30 py-8 border-b border-border/40">
          <div className="container mx-auto px-4">
            <Link
              to="/companies"
              className="inline-flex items-center gap-2 text-sm text-muted-foreground hover:text-foreground mb-6 transition-colors"
            >
              <ArrowLeft className="h-4 w-4" />
              Back to Companies
            </Link>

            <div className="flex flex-col lg:flex-row gap-6 lg:items-start">
              {/* Company Logo */}
              <div className="flex h-24 w-24 shrink-0 items-center justify-center rounded-2xl bg-card border border-border/50 shadow-sm overflow-hidden">
                {company.logo ? (
                  <img src={company.logo} alt={company.name} className="h-full w-full object-cover" />
                ) : (
                  <Building2 className="h-12 w-12 text-muted-foreground" />
                )}
              </div>

              {/* Company Info */}
              <div className="flex-1">
                <div className="flex flex-wrap items-start justify-between gap-4 mb-4">
                  <div>
                    <div className="flex items-center gap-2 mb-2">
                      <h1 className="text-2xl md:text-3xl font-bold text-foreground">
                        {company.name}
                      </h1>
                      {company.isVerified && (
                        <CheckCircle2 className="h-6 w-6 text-primary fill-primary/20" />
                      )}
                    </div>
                    <p className="text-lg text-muted-foreground">{company.industry}</p>
                  </div>

                  {company.website && (
                    <Button asChild>
                      <a href={company.website} target="_blank" rel="noopener noreferrer">
                        <Globe className="h-4 w-4 mr-2" />
                        Visit Website
                        <ExternalLink className="h-4 w-4 ml-2" />
                      </a>
                    </Button>
                  )}
                </div>

                <div className="flex flex-wrap gap-4 text-sm text-muted-foreground mb-6">
                  <span className="flex items-center gap-1.5">
                    <MapPin className="h-4 w-4" />
                    {company.location}
                  </span>
                  {company.size && (
                    <span className="flex items-center gap-1.5">
                      <Users className="h-4 w-4" />
                      {company.size} employees
                    </span>
                  )}
                  {company.foundedYear && (
                    <span className="flex items-center gap-1.5">
                      <Calendar className="h-4 w-4" />
                      Founded {company.foundedYear}
                    </span>
                  )}
                  <span className="flex items-center gap-1.5">
                    <Briefcase className="h-4 w-4" />
                    {company.openJobsCount} open positions
                  </span>
                </div>

                <div className="flex flex-wrap items-center gap-4">
                  <div className="flex items-center gap-1.5">
                    <Star className="h-5 w-5 text-warning fill-warning" />
                    <span className="font-semibold text-foreground">
                      {company.rating?.toFixed(1) || "N/A"}
                    </span>
                    <span className="text-muted-foreground">
                      ({company.reviewCount} reviews)
                    </span>
                  </div>
                  <Badge variant="secondary">{company.industry}</Badge>
                </div>
              </div>
            </div>
          </div>
        </section>

        {/* Content */}
        <section className="py-8">
          <div className="container mx-auto px-4">
            <div className="flex flex-col lg:flex-row gap-8">
              {/* Main Content */}
              <div className="flex-1 space-y-8">
                {/* About */}
                <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                  <h2 className="text-xl font-semibold text-foreground mb-4">
                    About {company.name}
                  </h2>
                  <p className="text-muted-foreground leading-relaxed whitespace-pre-line">
                    {company.description || "No description available."}
                  </p>
                </div>

                {/* Open Positions */}
                {company.recentJobs && company.recentJobs.length > 0 && (
                  <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                    <div className="flex items-center justify-between mb-6">
                      <h2 className="text-xl font-semibold text-foreground">
                        Open Positions
                      </h2>
                      <Badge variant="accent">
                        {company.openJobsCount} jobs
                      </Badge>
                    </div>
                    <div className="space-y-4">
                      {company.recentJobs.map((job) => (
                        <Link
                          key={job.id}
                          to={`/jobs/${job.id}`}
                          className="block p-4 rounded-xl border border-border/50 hover:border-accent/50 hover:bg-muted/50 transition-all"
                        >
                          <div className="flex flex-col md:flex-row md:items-center justify-between gap-3">
                            <div>
                              <h3 className="font-semibold text-foreground mb-1">
                                {job.title}
                              </h3>
                              <div className="flex flex-wrap gap-3 text-sm text-muted-foreground">
                                <span className="flex items-center gap-1">
                                  <MapPin className="h-3.5 w-3.5" />
                                  {job.location}
                                </span>
                                <span className="flex items-center gap-1">
                                  <Clock className="h-3.5 w-3.5" />
                                  {job.type}
                                </span>
                                {job.salary && (
                                  <span className="flex items-center gap-1">
                                    <DollarSign className="h-3.5 w-3.5" />
                                    {job.salary}
                                  </span>
                                )}
                              </div>
                            </div>
                            <Button variant="outline" size="sm">
                              View Job
                            </Button>
                          </div>
                        </Link>
                      ))}
                    </div>
                    {company.openJobsCount > company.recentJobs.length && (
                      <div className="mt-6 text-center">
                        <Button variant="outline" asChild>
                          <Link to={`/jobs?companyId=${company.id}`}>
                            View All {company.openJobsCount} Jobs
                          </Link>
                        </Button>
                      </div>
                    )}
                  </div>
                )}
              </div>

              {/* Sidebar */}
              <aside className="lg:w-80 shrink-0 space-y-6">
                {/* Company Info Card */}
                <div className="bg-card rounded-2xl border border-border/50 p-6">
                  <h3 className="font-semibold text-foreground mb-4">
                    Company Information
                  </h3>
                  <div className="space-y-4">
                    <div className="flex items-start gap-3">
                      <Building2 className="h-5 w-5 text-muted-foreground shrink-0 mt-0.5" />
                      <div>
                        <p className="text-sm text-muted-foreground">Industry</p>
                        <p className="text-foreground">{company.industry}</p>
                      </div>
                    </div>
                    <div className="flex items-start gap-3">
                      <MapPin className="h-5 w-5 text-muted-foreground shrink-0 mt-0.5" />
                      <div>
                        <p className="text-sm text-muted-foreground">Location</p>
                        <p className="text-foreground">{company.location}</p>
                      </div>
                    </div>
                    {company.size && (
                      <div className="flex items-start gap-3">
                        <Users className="h-5 w-5 text-muted-foreground shrink-0 mt-0.5" />
                        <div>
                          <p className="text-sm text-muted-foreground">Company Size</p>
                          <p className="text-foreground">{company.size}</p>
                        </div>
                      </div>
                    )}
                    {company.foundedYear && (
                      <div className="flex items-start gap-3">
                        <Calendar className="h-5 w-5 text-muted-foreground shrink-0 mt-0.5" />
                        <div>
                          <p className="text-sm text-muted-foreground">Founded</p>
                          <p className="text-foreground">{company.foundedYear}</p>
                        </div>
                      </div>
                    )}
                    {company.website && (
                      <div className="flex items-start gap-3">
                        <Globe className="h-5 w-5 text-muted-foreground shrink-0 mt-0.5" />
                        <div>
                          <p className="text-sm text-muted-foreground">Website</p>
                          <a
                            href={company.website}
                            target="_blank"
                            rel="noopener noreferrer"
                            className="text-primary hover:underline break-all"
                          >
                            {company.website.replace(/^https?:\/\//, '')}
                          </a>
                        </div>
                      </div>
                    )}
                  </div>
                </div>

                {/* Rating Card */}
                <div className="bg-card rounded-2xl border border-border/50 p-6">
                  <h3 className="font-semibold text-foreground mb-4">
                    Company Rating
                  </h3>
                  <div className="text-center">
                    <div className="flex items-center justify-center gap-2 mb-2">
                      <Star className="h-8 w-8 text-warning fill-warning" />
                      <span className="text-4xl font-bold text-foreground">
                        {company.rating?.toFixed(1) || "N/A"}
                      </span>
                    </div>
                    <p className="text-muted-foreground">
                      Based on {company.reviewCount} reviews
                    </p>
                  </div>
                </div>
              </aside>
            </div>
          </div>
        </section>
      </main>

      <Footer />
    </div>
  );
};

export default CompanyDetails;
