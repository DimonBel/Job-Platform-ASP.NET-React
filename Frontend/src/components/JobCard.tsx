import { Link } from "react-router-dom";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { MapPin, Clock, DollarSign, Bookmark, Building2 } from "lucide-react";

export interface Job {
  id: string;
  title: string;
  company: string;
  companyLogo?: string;
  location: string;
  type: "Full-time" | "Part-time" | "Contract" | "Remote" | "Hybrid";
  salary: string;
  postedAt: string;
  tags: string[];
  featured?: boolean;
}

interface JobCardProps {
  job: Job;
}

const JobCard = ({ job }: JobCardProps) => {
  return (
    <div
      className={`group relative rounded-2xl border bg-card p-6 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 ${
        job.featured
          ? "border-accent/30 bg-gradient-to-br from-accent/5 to-transparent"
          : "border-border/50 hover:border-accent/20"
      }`}
    >
      {job.featured && (
        <div className="absolute -top-3 left-6">
          <Badge variant="accent" className="shadow-md shadow-accent/20">
            Featured
          </Badge>
        </div>
      )}

      <div className="flex gap-4">
        {/* Company Logo */}
        <div className="flex h-14 w-14 shrink-0 items-center justify-center rounded-xl bg-muted border border-border/50">
          {job.companyLogo ? (
            <img
              src={job.companyLogo}
              alt={job.company}
              className="h-8 w-8 object-contain"
            />
          ) : (
            <Building2 className="h-6 w-6 text-muted-foreground" />
          )}
        </div>

        {/* Content */}
        <div className="flex-1 min-w-0">
          <div className="flex items-start justify-between gap-4">
            <div>
              <Link
                to={`/jobs/${job.id}`}
                className="inline-block text-lg font-semibold text-foreground hover:text-accent transition-colors line-clamp-1"
              >
                {job.title}
              </Link>
              <p className="text-sm text-muted-foreground">{job.company}</p>
            </div>
            <Button
              variant="ghost"
              size="icon"
              className="shrink-0 text-muted-foreground hover:text-accent"
            >
              <Bookmark className="h-5 w-5" />
            </Button>
          </div>

          {/* Meta Info */}
          <div className="mt-3 flex flex-wrap items-center gap-x-4 gap-y-2 text-sm text-muted-foreground">
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
              {job.postedAt}
            </span>
          </div>

          {/* Tags */}
          <div className="mt-4 flex flex-wrap gap-2">
            <Badge variant="secondary">{job.type}</Badge>
            {job.tags.slice(0, 3).map((tag) => (
              <Badge key={tag} variant="muted">
                {tag}
              </Badge>
            ))}
          </div>
        </div>
      </div>

      {/* Apply Button - appears on hover */}
      <div className="mt-4 pt-4 border-t border-border/50 flex items-center justify-between">
        <span className="text-xs text-muted-foreground">
          {Math.floor(Math.random() * 50) + 10} applicants
        </span>
        <Button variant="accent" size="sm" asChild>
          <Link to={`/jobs/${job.id}`}>View Details</Link>
        </Button>
      </div>
    </div>
  );
};

export default JobCard;
