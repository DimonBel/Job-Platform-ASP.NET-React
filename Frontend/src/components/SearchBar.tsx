import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Search, MapPin } from "lucide-react";

interface SearchBarProps {
  variant?: "hero" | "compact";
  className?: string;
}

const SearchBar = ({ variant = "hero", className = "" }: SearchBarProps) => {
  const [keyword, setKeyword] = useState("");
  const [location, setLocation] = useState("");
  const navigate = useNavigate();

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    const params = new URLSearchParams();
    if (keyword) params.set("q", keyword);
    if (location) params.set("location", location);
    navigate(`/jobs?${params.toString()}`);
  };

  if (variant === "compact") {
    return (
      <form onSubmit={handleSearch} className={`flex gap-2 ${className}`}>
        <div className="relative flex-1">
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
          <Input
            type="text"
            placeholder="Search jobs..."
            value={keyword}
            onChange={(e) => setKeyword(e.target.value)}
            className="pl-10"
          />
        </div>
        <Button type="submit" variant="accent">
          Search
        </Button>
      </form>
    );
  }

  return (
    <form
      onSubmit={handleSearch}
      className={`w-full max-w-4xl mx-auto ${className}`}
    >
      <div className="flex flex-col md:flex-row gap-3 p-3 rounded-2xl bg-card shadow-xl shadow-primary/5 border border-border/50">
        {/* Keyword Input */}
        <div className="relative flex-1">
          <Search className="absolute left-4 top-1/2 -translate-y-1/2 h-5 w-5 text-muted-foreground" />
          <Input
            type="text"
            placeholder="Job title, keyword, or company"
            value={keyword}
            onChange={(e) => setKeyword(e.target.value)}
            className="h-14 pl-12 border-0 bg-transparent text-base focus-visible:ring-0 focus-visible:ring-offset-0"
          />
        </div>

        {/* Divider */}
        <div className="hidden md:block w-px bg-border" />

        {/* Location Input */}
        <div className="relative flex-1">
          <MapPin className="absolute left-4 top-1/2 -translate-y-1/2 h-5 w-5 text-muted-foreground" />
          <Input
            type="text"
            placeholder="City, state, or remote"
            value={location}
            onChange={(e) => setLocation(e.target.value)}
            className="h-14 pl-12 border-0 bg-transparent text-base focus-visible:ring-0 focus-visible:ring-offset-0"
          />
        </div>

        {/* Search Button */}
        <Button
          type="submit"
          variant="hero"
          size="xl"
          className="md:w-auto w-full"
        >
          <Search className="h-5 w-5 mr-2" />
          Search Jobs
        </Button>
      </div>
    </form>
  );
};

export default SearchBar;
