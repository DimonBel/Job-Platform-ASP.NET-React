interface StatCardProps {
  value: string;
  label: string;
  icon?: React.ReactNode;
}

const StatCard = ({ value, label, icon }: StatCardProps) => {
  return (
    <div className="text-center">
      <div className="flex items-center justify-center gap-2 mb-1">
        {icon}
        <span className="text-3xl md:text-4xl font-bold text-primary-foreground">{value}</span>
      </div>
      <p className="text-sm text-primary-foreground/70">{label}</p>
    </div>
  );
};

export default StatCard;
