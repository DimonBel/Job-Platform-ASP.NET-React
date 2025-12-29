import { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import SearchBar from "@/components/SearchBar";
import JobCard from "@/components/JobCard";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Checkbox } from "@/components/ui/checkbox";
import { Skeleton } from "@/components/ui/skeleton";
import { useJobSearch } from "@/services/hooks";
import type { JobSearchParams } from "@/services/api";
import {
  Filter,
  X,
  SlidersHorizontal,
  MapPin,
  Briefcase,
  Clock,
  ChevronLeft,
  ChevronRight,
} from "lucide-react";

const jobTypes = ["Full-time", "Part-time", "Contract", "Remote", "Hybrid"];
const experienceLevels = ["Entry Level", "Mid Level", "Senior", "Lead", "Executive"];
const locations = ["San Francisco, CA", "New York, NY", "Remote", "Austin, TX", "Seattle, WA", "Los Angeles, CA"];

const Jobs = () => {
  const [searchParams] = useSearchParams();
  const [showFilters, setShowFilters] = useState(false);
  const [selectedTypes, setSelectedTypes] = useState<string[]>([]);
  const [selectedExperience, setSelectedExperience] = useState<string[]>([]);
  const [selectedLocations, setSelectedLocations] = useState<string[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 10;

  const query = searchParams.get("q") || "";
  const locationQuery = searchParams.get("location") || "";

  // Build search params for API
  const searchApiParams: JobSearchParams = {
    page: currentPage,
    pageSize,
    query: query || undefined,
    location: locationQuery || undefined,
    jobTypes: selectedTypes.length > 0 ? selectedTypes : undefined,
    experienceLevels: selectedExperience.length > 0 ? selectedExperience : undefined,
    locations: selectedLocations.length > 0 ? selectedLocations : undefined,
  };

  const { data: jobsData, isLoading, error } = useJobSearch(searchApiParams);

  // Reset to page 1 when filters change
  useEffect(() => {
    setCurrentPage(1);
  }, [query, locationQuery, selectedTypes, selectedExperience, selectedLocations]);

  const toggleFilter = (value: string, selected: string[], setSelected: (val: string[]) => void) => {
    if (selected.includes(value)) {
      setSelected(selected.filter(v => v !== value));
    } else {
      setSelected([...selected, value]);
    }
  };

  const clearFilters = () => {
    setSelectedTypes([]);
    setSelectedExperience([]);
    setSelectedLocations([]);
  };

  const activeFiltersCount = selectedTypes.length + selectedExperience.length + selectedLocations.length;

  return (
    <div className="min-h-screen flex flex-col bg-background">
      <Navbar />

      <main className="flex-1">
        {/* Header */}
        <section className="bg-muted/30 py-8 border-b border-border/40">
          <div className="container mx-auto px-4">
            <h1 className="text-3xl font-bold text-foreground mb-2">
              {query ? `Results for "${query}"` : "All Jobs"}
            </h1>
            <p className="text-muted-foreground mb-6">
              {isLoading ? (
                <Skeleton className="h-5 w-32 inline-block" />
              ) : (
                <>
                  {jobsData?.totalCount || 0} jobs found
                  {locationQuery && ` in ${locationQuery}`}
                </>
              )}
            </p>
            <SearchBar variant="compact" />
          </div>
        </section>

        {/* Main Content */}
        <section className="py-8">
          <div className="container mx-auto px-4">
            <div className="flex gap-8">
              {/* Sidebar Filters - Desktop */}
              <aside className="hidden lg:block w-72 shrink-0">
                <div className="sticky top-24 space-y-6">
                  <div className="flex items-center justify-between">
                    <h2 className="font-semibold text-foreground flex items-center gap-2">
                      <Filter className="h-4 w-4" />
                      Filters
                    </h2>
                    {activeFiltersCount > 0 && (
                      <Button variant="ghost" size="sm" onClick={clearFilters}>
                        Clear all
                      </Button>
                    )}
                  </div>

                  {/* Job Type Filter */}
                  <div className="bg-card rounded-xl border border-border/50 p-5">
                    <h3 className="font-medium text-foreground mb-4 flex items-center gap-2">
                      <Briefcase className="h-4 w-4 text-muted-foreground" />
                      Job Type
                    </h3>
                    <div className="space-y-3">
                      {jobTypes.map((type) => (
                        <label
                          key={type}
                          className="flex items-center gap-3 cursor-pointer group"
                        >
                          <Checkbox
                            checked={selectedTypes.includes(type)}
                            onCheckedChange={() => toggleFilter(type, selectedTypes, setSelectedTypes)}
                          />
                          <span className="text-sm text-muted-foreground group-hover:text-foreground transition-colors">
                            {type}
                          </span>
                        </label>
                      ))}
                    </div>
                  </div>

                  {/* Experience Level Filter */}
                  <div className="bg-card rounded-xl border border-border/50 p-5">
                    <h3 className="font-medium text-foreground mb-4 flex items-center gap-2">
                      <Clock className="h-4 w-4 text-muted-foreground" />
                      Experience Level
                    </h3>
                    <div className="space-y-3">
                      {experienceLevels.map((level) => (
                        <label
                          key={level}
                          className="flex items-center gap-3 cursor-pointer group"
                        >
                          <Checkbox
                            checked={selectedExperience.includes(level)}
                            onCheckedChange={() => toggleFilter(level, selectedExperience, setSelectedExperience)}
                          />
                          <span className="text-sm text-muted-foreground group-hover:text-foreground transition-colors">
                            {level}
                          </span>
                        </label>
                      ))}
                    </div>
                  </div>

                  {/* Salary Range Filter - Disabled (Salary is a string field) */}
                  {/* <div className="bg-card rounded-xl border border-border/50 p-5">
                    <h3 className="font-medium text-foreground mb-4 flex items-center gap-2">
                      <DollarSign className="h-4 w-4 text-muted-foreground" />
                      Salary Range
                    </h3>
                    <div className="space-y-4">
                      <Slider
                        value={salaryRange}
                        onValueChange={setSalaryRange}
                        min={30000}
                        max={300000}
                        step={10000}
                        className="mt-2"
                      />
                      <div className="flex items-center justify-between text-sm text-muted-foreground">
                        <span>${(salaryRange[0] / 1000).toFixed(0)}k</span>
                        <span>${(salaryRange[1] / 1000).toFixed(0)}k</span>
                      </div>
                    </div>
                  </div> */}

                  {/* Location Filter */}
                  <div className="bg-card rounded-xl border border-border/50 p-5">
                    <h3 className="font-medium text-foreground mb-4 flex items-center gap-2">
                      <MapPin className="h-4 w-4 text-muted-foreground" />
                      Location
                    </h3>
                    <div className="space-y-3">
                      {locations.map((location) => (
                        <label
                          key={location}
                          className="flex items-center gap-3 cursor-pointer group"
                        >
                          <Checkbox
                            checked={selectedLocations.includes(location)}
                            onCheckedChange={() => toggleFilter(location, selectedLocations, setSelectedLocations)}
                          />
                          <span className="text-sm text-muted-foreground group-hover:text-foreground transition-colors">
                            {location}
                          </span>
                        </label>
                      ))}
                    </div>
                  </div>
                </div>
              </aside>

              {/* Job Listings */}
              <div className="flex-1">
                {/* Mobile Filter Toggle */}
                <div className="lg:hidden mb-6">
                  <Button
                    variant="outline"
                    onClick={() => setShowFilters(!showFilters)}
                    className="w-full justify-between"
                  >
                    <span className="flex items-center gap-2">
                      <SlidersHorizontal className="h-4 w-4" />
                      Filters
                    </span>
                    {activeFiltersCount > 0 && (
                      <Badge variant="accent">{activeFiltersCount}</Badge>
                    )}
                  </Button>
                </div>

                {/* Active Filters */}
                {activeFiltersCount > 0 && (
                  <div className="flex flex-wrap gap-2 mb-6">
                    {selectedTypes.map((type) => (
                      <Badge
                        key={type}
                        variant="secondary"
                        className="cursor-pointer hover:bg-destructive/10 hover:text-destructive"
                        onClick={() => toggleFilter(type, selectedTypes, setSelectedTypes)}
                      >
                        {type}
                        <X className="h-3 w-3 ml-1" />
                      </Badge>
                    ))}
                    {selectedExperience.map((exp) => (
                      <Badge
                        key={exp}
                        variant="secondary"
                        className="cursor-pointer hover:bg-destructive/10 hover:text-destructive"
                        onClick={() => toggleFilter(exp, selectedExperience, setSelectedExperience)}
                      >
                        {exp}
                        <X className="h-3 w-3 ml-1" />
                      </Badge>
                    ))}
                    {selectedLocations.map((loc) => (
                      <Badge
                        key={loc}
                        variant="secondary"
                        className="cursor-pointer hover:bg-destructive/10 hover:text-destructive"
                        onClick={() => toggleFilter(loc, selectedLocations, setSelectedLocations)}
                      >
                        {loc}
                        <X className="h-3 w-3 ml-1" />
                      </Badge>
                    ))}
                  </div>
                )}

                {/* Job Grid */}
                <div className="space-y-4">
                  {isLoading ? (
                    // Loading skeletons
                    Array.from({ length: 5 }).map((_, index) => (
                      <div key={index} className="bg-card rounded-xl border border-border/50 p-6">
                        <div className="flex gap-4">
                          <Skeleton className="h-14 w-14 rounded-xl" />
                          <div className="flex-1 space-y-2">
                            <Skeleton className="h-5 w-48" />
                            <Skeleton className="h-4 w-32" />
                            <div className="flex gap-2">
                              <Skeleton className="h-6 w-20" />
                              <Skeleton className="h-6 w-24" />
                              <Skeleton className="h-6 w-28" />
                            </div>
                          </div>
                        </div>
                      </div>
                    ))
                  ) : error ? (
                    <div className="text-center py-16">
                      <div className="w-16 h-16 mx-auto mb-4 rounded-full bg-destructive/10 flex items-center justify-center">
                        <X className="h-8 w-8 text-destructive" />
                      </div>
                      <h3 className="text-lg font-medium text-foreground mb-2">
                        Error loading jobs
                      </h3>
                      <p className="text-muted-foreground mb-4">
                        Please try again later
                      </p>
                    </div>
                  ) : jobsData?.items && jobsData.items.length > 0 ? (
                    <>
                      {jobsData.items.map((job, index) => (
                        <div
                          key={job.id}
                          className="animate-fade-in"
                          style={{ animationDelay: `${index * 0.05}s` }}
                        >
                          <JobCard job={{
                            id: job.id,
                            title: job.title,
                            company: job.companyName,
                            location: job.location,
                            salary: job.salary,
                            type: job.jobType,
                            posted: job.postedDate,
                            logo: job.companyLogo || "/placeholder.svg",
                            tags: job.tags,
                            featured: job.isFeatured,
                          }} />
                        </div>
                      ))}
                      
                      {/* Pagination */}
                      {jobsData.totalPages > 1 && (
                        <div className="flex items-center justify-center gap-2 pt-6">
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => setCurrentPage(p => Math.max(1, p - 1))}
                            disabled={currentPage === 1}
                          >
                            <ChevronLeft className="h-4 w-4" />
                            Previous
                          </Button>
                          <div className="flex items-center gap-1">
                            {Array.from({ length: Math.min(5, jobsData.totalPages) }, (_, i) => {
                              let pageNum: number;
                              if (jobsData.totalPages <= 5) {
                                pageNum = i + 1;
                              } else if (currentPage <= 3) {
                                pageNum = i + 1;
                              } else if (currentPage >= jobsData.totalPages - 2) {
                                pageNum = jobsData.totalPages - 4 + i;
                              } else {
                                pageNum = currentPage - 2 + i;
                              }
                              return (
                                <Button
                                  key={pageNum}
                                  variant={currentPage === pageNum ? "default" : "outline"}
                                  size="sm"
                                  onClick={() => setCurrentPage(pageNum)}
                                  className="w-9"
                                >
                                  {pageNum}
                                </Button>
                              );
                            })}
                          </div>
                          <Button
                            variant="outline"
                            size="sm"
                            onClick={() => setCurrentPage(p => Math.min(jobsData.totalPages, p + 1))}
                            disabled={currentPage === jobsData.totalPages}
                          >
                            Next
                            <ChevronRight className="h-4 w-4" />
                          </Button>
                        </div>
                      )}
                    </>
                  ) : (
                    <div className="text-center py-16">
                      <div className="w-16 h-16 mx-auto mb-4 rounded-full bg-muted flex items-center justify-center">
                        <Briefcase className="h-8 w-8 text-muted-foreground" />
                      </div>
                      <h3 className="text-lg font-medium text-foreground mb-2">
                        No jobs found
                      </h3>
                      <p className="text-muted-foreground mb-4">
                        Try adjusting your search or filters
                      </p>
                      <Button variant="outline" onClick={clearFilters}>
                        Clear Filters
                      </Button>
                    </div>
                  )}
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

export default Jobs;
