import { useParams, Link } from "react-router-dom";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Skeleton } from "@/components/ui/skeleton";
import { useJobDetails } from "@/services/hooks";
import {
  MapPin,
  Clock,
  DollarSign,
  Bookmark,
  Share2,
  Building2,
  Users,
  Calendar,
  Briefcase,
  CheckCircle2,
  ArrowLeft,
  ExternalLink,
  Globe,
  Mail,
} from "lucide-react";

const JobDetails = () => {
  const { id } = useParams();
  const { data: job, isLoading, error } = useJobDetails(id || "");

  if (isLoading) {
    return (
      <div className="min-h-screen flex flex-col bg-background">
        <Navbar />
        <main className="flex-1">
          <section className="bg-muted/30 py-8 border-b border-border/40">
            <div className="container mx-auto px-4">
              <Skeleton className="h-5 w-24 mb-6" />
              <div className="flex flex-col lg:flex-row gap-6 lg:items-start">
                <Skeleton className="h-20 w-20 rounded-2xl" />
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
                  {Array.from({ length: 4 }).map((_, i) => (
                    <div key={i} className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                      <Skeleton className="h-6 w-48 mb-4" />
                      <div className="space-y-3">
                        <Skeleton className="h-4 w-full" />
                        <Skeleton className="h-4 w-3/4" />
                        <Skeleton className="h-4 w-5/6" />
                      </div>
                    </div>
                  ))}
                </div>
                <aside className="lg:w-80 shrink-0 space-y-6">
                  <Skeleton className="h-48 w-full rounded-2xl" />
                  <Skeleton className="h-64 w-full rounded-2xl" />
                </aside>
              </div>
            </div>
          </section>
        </main>
        <Footer />
      </div>
    );
  }

  if (error || !job) {
    return (
      <div className="min-h-screen flex flex-col">
        <Navbar />
        <main className="flex-1 flex items-center justify-center">
          <div className="text-center">
            <h1 className="text-2xl font-bold text-foreground mb-4">Job not found</h1>
            <Button asChild>
              <Link to="/jobs">Back to Jobs</Link>
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
              to="/jobs"
              className="inline-flex items-center gap-2 text-sm text-muted-foreground hover:text-foreground mb-6 transition-colors"
            >
              <ArrowLeft className="h-4 w-4" />
              Back to Jobs
            </Link>

            <div className="flex flex-col lg:flex-row gap-6 lg:items-start">
              {/* Company Logo */}
              <div className="flex h-20 w-20 shrink-0 items-center justify-center rounded-2xl bg-card border border-border/50 shadow-sm overflow-hidden">
                {job.companyLogo ? (
                  <img src={job.companyLogo} alt={job.companyName} className="h-full w-full object-cover" />
                ) : (
                  <Building2 className="h-10 w-10 text-muted-foreground" />
                )}
              </div>

              {/* Job Info */}
              <div className="flex-1">
                <div className="flex flex-wrap items-start justify-between gap-4 mb-4">
                  <div>
                    {job.isFeatured && (
                      <Badge variant="accent" className="mb-2">Featured</Badge>
                    )}
                    <h1 className="text-2xl md:text-3xl font-bold text-foreground mb-2">
                      {job.title}
                    </h1>
                    <p className="text-lg text-muted-foreground">{job.companyName}</p>
                  </div>

                  <div className="flex gap-2">
                    <Button variant="outline" size="icon">
                      <Bookmark className="h-5 w-5" />
                    </Button>
                    <Button variant="outline" size="icon">
                      <Share2 className="h-5 w-5" />
                    </Button>
                  </div>
                </div>

                <div className="flex flex-wrap gap-4 text-sm text-muted-foreground mb-6">
                  <span className="flex items-center gap-1.5">
                    <MapPin className="h-4 w-4" />
                    {job.location}
                  </span>
                  <span className="flex items-center gap-1.5">
                    <DollarSign className="h-4 w-4" />
                    {job.salary}
                  </span>
                  <span className="flex items-center gap-1.5">
                    <Clock className="h-4 w-4" />
                    {job.postedDate}
                  </span>
                  <span className="flex items-center gap-1.5">
                    <Users className="h-4 w-4" />
                    {job.applicantCount || 0} applicants
                  </span>
                </div>

                <div className="flex flex-wrap gap-2">
                  <Badge variant="secondary">{job.jobType}</Badge>
                  {job.tags.map((tag) => (
                    <Badge key={tag} variant="muted">
                      {tag}
                    </Badge>
                  ))}
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
                {/* Description */}
                <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                  <h2 className="text-xl font-semibold text-foreground mb-4">
                    About This Role
                  </h2>
                  <p className="text-muted-foreground leading-relaxed">
                    {job.description}
                  </p>
                </div>

                {/* Responsibilities */}
                {job.responsibilities && job.responsibilities.length > 0 && (
                  <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                    <h2 className="text-xl font-semibold text-foreground mb-4">
                      Key Responsibilities
                    </h2>
                    <ul className="space-y-3">
                      {job.responsibilities.map((item, i) => (
                        <li key={i} className="flex items-start gap-3">
                          <CheckCircle2 className="h-5 w-5 text-accent shrink-0 mt-0.5" />
                          <span className="text-muted-foreground">{item}</span>
                        </li>
                      ))}
                    </ul>
                  </div>
                )}

                {/* Requirements */}
                {job.requirements && job.requirements.length > 0 && (
                  <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                    <h2 className="text-xl font-semibold text-foreground mb-4">
                      Requirements
                    </h2>
                    <ul className="space-y-3">
                      {job.requirements.map((item, i) => (
                        <li key={i} className="flex items-start gap-3">
                          <CheckCircle2 className="h-5 w-5 text-accent shrink-0 mt-0.5" />
                          <span className="text-muted-foreground">{item}</span>
                        </li>
                      ))}
                    </ul>
                  </div>
                )}

                {/* Benefits */}
                {job.benefits && job.benefits.length > 0 && (
                  <div className="bg-card rounded-2xl border border-border/50 p-6 md:p-8">
                    <h2 className="text-xl font-semibold text-foreground mb-4">
                      Benefits & Perks
                    </h2>
                    <ul className="space-y-3">
                      {job.benefits.map((item, i) => (
                        <li key={i} className="flex items-start gap-3">
                          <CheckCircle2 className="h-5 w-5 text-accent shrink-0 mt-0.5" />
                          <span className="text-muted-foreground">{item}</span>
                        </li>
                      ))}
                    </ul>
                  </div>
                )}
              </div>

              {/* Sidebar */}
              <aside className="lg:w-80 shrink-0 space-y-6">
                {/* Apply Card */}
                <div className="sticky top-24 space-y-6">
                  <div className="bg-card rounded-2xl border border-border/50 p-6">
                    <Button variant="hero" size="lg" className="w-full mb-4">
                      Apply Now
                    </Button>
                    <Button variant="outline" size="lg" className="w-full">
                      <Mail className="h-4 w-4 mr-2" />
                      Contact Recruiter
                    </Button>
                    <p className="text-xs text-muted-foreground text-center mt-4">
                      Apply within the next 7 days for priority review
                    </p>
                  </div>

                  {/* Company Info */}
                  <div className="bg-card rounded-2xl border border-border/50 p-6">
                    <h3 className="font-semibold text-foreground mb-4">
                      About {job.companyName}
                    </h3>
                    <p className="text-sm text-muted-foreground mb-4">
                      {job.companyDescription || `${job.companyName} is a leading company in the industry, dedicated to innovation and excellence.`}
                    </p>
                    <div className="space-y-3 text-sm">
                      {job.companySize && (
                        <div className="flex items-center gap-3 text-muted-foreground">
                          <Users className="h-4 w-4" />
                          {job.companySize}
                        </div>
                      )}
                      <div className="flex items-center gap-3 text-muted-foreground">
                        <MapPin className="h-4 w-4" />
                        {job.location}
                      </div>
                      {job.companyWebsite && (
                        <div className="flex items-center gap-3 text-muted-foreground">
                          <Globe className="h-4 w-4" />
                          <a
                            href={job.companyWebsite}
                            className="text-accent hover:underline"
                            target="_blank"
                            rel="noopener noreferrer"
                          >
                            Visit Website
                            <ExternalLink className="h-3 w-3 inline ml-1" />
                          </a>
                        </div>
                      )}
                    </div>
                    <Button variant="outline" className="w-full mt-4" asChild>
                      <Link to="/companies">View Company Profile</Link>
                    </Button>
                  </div>

                  {/* Job Highlights */}
                  <div className="bg-accent/5 rounded-2xl border border-accent/20 p-6">
                    <h3 className="font-semibold text-foreground mb-4">
                      Job Highlights
                    </h3>
                    <div className="space-y-3 text-sm">
                      <div className="flex items-center gap-3">
                        <Briefcase className="h-4 w-4 text-accent" />
                        <span className="text-muted-foreground">{job.jobType}</span>
                      </div>
                      <div className="flex items-center gap-3">
                        <DollarSign className="h-4 w-4 text-accent" />
                        <span className="text-muted-foreground">{job.salary}</span>
                      </div>
                      <div className="flex items-center gap-3">
                        <Calendar className="h-4 w-4 text-accent" />
                        <span className="text-muted-foreground">Posted {job.postedDate}</span>
                      </div>
                    </div>
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

export default JobDetails;
