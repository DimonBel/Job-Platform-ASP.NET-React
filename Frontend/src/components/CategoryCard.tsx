import { Link } from "react-router-dom";
import { LucideIcon } from "lucide-react";

interface CategoryCardProps {
  icon: LucideIcon;
  title: string;
  count: number;
  color: string;
}

const CategoryCard = ({ icon: Icon, title, count, color }: CategoryCardProps) => {
  return (
    <Link
      to={`/jobs?category=${encodeURIComponent(title)}`}
      className="group flex flex-col items-center p-6 rounded-2xl bg-card border border-border/50 transition-all duration-300 hover:shadow-lg hover:-translate-y-1 hover:border-accent/20"
    >
      <div
        className={`flex h-14 w-14 items-center justify-center rounded-xl ${color} mb-4 transition-transform group-hover:scale-110`}
      >
        <Icon className="h-7 w-7" />
      </div>
      <h3 className="font-semibold text-foreground text-center mb-1">{title}</h3>
      <p className="text-sm text-muted-foreground">{count.toLocaleString()} jobs</p>
    </Link>
  );
};

export default CategoryCard;
