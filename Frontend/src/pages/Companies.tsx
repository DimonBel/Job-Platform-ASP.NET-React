import { useState } from "react";
import { Link } from "react-router-dom";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Badge } from "@/components/ui/badge";
import { Skeleton } from "@/components/ui/skeleton";
import { useCompanySearch } from "@/services/hooks";
import {
  Search,
  Building2,
  MapPin,
  Users,
  Briefcase,
  Star,
  ArrowRight,
  X,
} from "lucide-react";

const Companies = () => {
  const [searchQuery, setSearchQuery] = useState("");
  const { data: companiesData, isLoading, error } = useCompanySearch({ 
    query: searchQuery || undefined,
    pageSize: 50 
  });

  return (
    <div className="min-h-screen flex flex-col bg-background">
      <Navbar />

      <main className="flex-1">
        {/* Header */}
        <section className="bg-muted/30 py-12 border-b border-border/40">
          <div className="container mx-auto px-4 text-center">
            <h1 className="text-3xl md:text-4xl font-bold text-foreground mb-4">
              Discover Great Companies
            </h1>
            <p className="text-lg text-muted-foreground mb-8 max-w-2xl mx-auto">
              Explore company profiles, reviews, and open positions at top employers
            </p>

            <div className="relative max-w-xl mx-auto">
              <Search className="absolute left-4 top-1/2 -translate-y-1/2 h-5 w-5 text-muted-foreground" />
              <Input
                type="text"
                placeholder="Search companies by name or industry..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="h-14 pl-12 text-base rounded-xl"
              />
            </div>
          </div>
        </section>

        {/* Companies Grid */}
        <section className="py-12">
          <div className="container mx-auto px-4">
            <div className="flex items-center justify-between mb-8">
              <p className="text-muted-foreground">
                {isLoading ? (
                  <Skeleton className="h-5 w-32 inline-block" />
                ) : (
                  `${companiesData?.totalCount || 0} companies found`
                )}
              </p>
            </div>

            {isLoading ? (
              // Loading skeletons
              <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
                {Array.from({ length: 6 }).map((_, index) => (
                  <div key={index} className="bg-card rounded-2xl border border-border/50 p-6">
                    <div className="flex items-start gap-4 mb-4">
                      <Skeleton className="h-14 w-14 rounded-xl" />
                      <div className="flex-1 space-y-2">
                        <Skeleton className="h-5 w-32" />
                        <Skeleton className="h-4 w-24" />
                      </div>
                    </div>
                    <Skeleton className="h-10 w-full mb-4" />
                    <div className="flex gap-3 mb-4">
                      <Skeleton className="h-5 w-28" />
                      <Skeleton className="h-5 w-24" />
                    </div>
                    <Skeleton className="h-10 w-full" />
                  </div>
                ))}
              </div>
            ) : error ? (
              <div className="text-center py-16">
                <div className="w-16 h-16 mx-auto mb-4 rounded-full bg-destructive/10 flex items-center justify-center">
                  <X className="h-8 w-8 text-destructive" />
                </div>
                <h3 className="text-lg font-medium text-foreground mb-2">
                  Error loading companies
                </h3>
                <p className="text-muted-foreground mb-4">
                  Please try again later
                </p>
              </div>
            ) : companiesData?.items && companiesData.items.length > 0 ? (
              <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
                {companiesData.items.map((company, index) => (
                  <div
                    key={company.id}
                    className="group bg-card rounded-2xl border border-border/50 p-6 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 hover:border-accent/20 animate-fade-in"
                    style={{ animationDelay: `${index * 0.1}s` }}
                  >
                    {/* Header */}
                    <div className="flex items-start gap-4 mb-4">
                      <div className="flex h-14 w-14 shrink-0 items-center justify-center rounded-xl bg-muted border border-border/50 overflow-hidden">
                        {company.logo ? (
                          <img src={company.logo} alt={company.name} className="h-full w-full object-cover" />
                        ) : (
                          <Building2 className="h-7 w-7 text-muted-foreground" />
                        )}
                      </div>
                      <div className="flex-1 min-w-0">
                        <h3 className="font-semibold text-foreground truncate">
                          {company.name}
                        </h3>
                        <p className="text-sm text-muted-foreground">
                          {company.industry}
                        </p>
                      </div>
                    </div>

                    {/* Description */}
                    <p className="text-sm text-muted-foreground mb-4 line-clamp-2">
                      {company.description}
                    </p>

                    {/* Meta */}
                    <div className="flex flex-wrap gap-3 text-sm text-muted-foreground mb-4">
                      <span className="flex items-center gap-1.5">
                        <MapPin className="h-4 w-4" />
                        {company.location}
                      </span>
                      {company.size && (
                        <span className="flex items-center gap-1.5">
                          <Users className="h-4 w-4" />
                          {company.size}
                        </span>
                      )}
                    </div>

                    {/* Rating & Jobs */}
                    <div className="flex items-center justify-between pt-4 border-t border-border/50">
                      <div className="flex items-center gap-1.5">
                        <Star className="h-4 w-4 text-warning fill-warning" />
                        <span className="font-medium text-foreground">
                          {company.rating?.toFixed(1) || "N/A"}
                        </span>
                        {company.reviewCount && (
                          <span className="text-sm text-muted-foreground">
                            ({company.reviewCount} reviews)
                          </span>
                        )}
                      </div>
                      <Badge variant="accent">
                        <Briefcase className="h-3 w-3 mr-1" />
                        {company.openJobsCount || 0} jobs
                      </Badge>
                    </div>

                    {/* View Button */}
                    <Button
                      variant="outline"
                      className="w-full mt-4 group-hover:bg-accent group-hover:text-accent-foreground group-hover:border-accent transition-colors"
                      asChild
                    >
                      <Link to={`/companies/${company.id}`}>
                        View Company
                        <ArrowRight className="h-4 w-4 ml-2" />
                      </Link>
                    </Button>
                  </div>
                ))}
              </div>
            ) : (
              <div className="text-center py-16">
                <div className="w-16 h-16 mx-auto mb-4 rounded-full bg-muted flex items-center justify-center">
                  <Building2 className="h-8 w-8 text-muted-foreground" />
                </div>
                <h3 className="text-lg font-medium text-foreground mb-2">
                  No companies found
                </h3>
                <p className="text-muted-foreground mb-4">
                  Try adjusting your search
                </p>
                <Button variant="outline" onClick={() => setSearchQuery("")}>
                  Clear Search
                </Button>
              </div>
            )}
          </div>
        </section>
      </main>

      <Footer />
    </div>
  );
};

export default Companies;
