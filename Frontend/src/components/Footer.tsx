import { Link } from "react-router-dom";
import { Briefcase, Github, Twitter, Linkedin, Mail } from "lucide-react";

const Footer = () => {
  const footerLinks = {
    "For Job Seekers": [
      { label: "Browse Jobs", path: "/jobs" },
      { label: "Career Advice", path: "/advice" },
      { label: "Resume Builder", path: "/resume" },
      { label: "Salary Calculator", path: "/salary" },
    ],
    "For Employers": [
      { label: "Post a Job", path: "/post-job" },
      { label: "Browse Candidates", path: "/candidates" },
      { label: "Pricing Plans", path: "/pricing" },
      { label: "Employer Resources", path: "/resources" },
    ],
    Company: [
      { label: "About Us", path: "/about" },
      { label: "Contact", path: "/contact" },
      { label: "Privacy Policy", path: "/privacy" },
      { label: "Terms of Service", path: "/terms" },
    ],
  };

  const socialLinks = [
    { icon: Twitter, href: "#", label: "Twitter" },
    { icon: Linkedin, href: "#", label: "LinkedIn" },
    { icon: Github, href: "#", label: "GitHub" },
    { icon: Mail, href: "#", label: "Email" },
  ];

  return (
    <footer className="border-t border-border/40 bg-muted/30">
      <div className="container mx-auto px-4 py-12">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-8 lg:gap-12">
          {/* Brand */}
          <div className="col-span-2 md:col-span-1">
            <Link to="/" className="flex items-center gap-2 mb-4">
              <div className="flex h-9 w-9 items-center justify-center rounded-xl bg-accent shadow-md shadow-accent/20">
                <Briefcase className="h-5 w-5 text-accent-foreground" />
              </div>
              <span className="text-xl font-bold text-foreground">
                Job<span className="text-accent">Flow</span>
              </span>
            </Link>
            <p className="text-sm text-muted-foreground mb-4 max-w-xs">
              Connecting talented professionals with their dream careers. Your journey to success starts here.
            </p>
            <div className="flex gap-3">
              {socialLinks.map((social) => (
                <a
                  key={social.label}
                  href={social.href}
                  className="flex h-9 w-9 items-center justify-center rounded-lg bg-background border border-border/50 text-muted-foreground hover:text-accent hover:border-accent/30 transition-colors"
                  aria-label={social.label}
                >
                  <social.icon className="h-4 w-4" />
                </a>
              ))}
            </div>
          </div>

          {/* Links */}
          {Object.entries(footerLinks).map(([title, links]) => (
            <div key={title}>
              <h3 className="font-semibold text-foreground mb-4">{title}</h3>
              <ul className="space-y-2.5">
                {links.map((link) => (
                  <li key={link.path}>
                    <Link
                      to={link.path}
                      className="text-sm text-muted-foreground hover:text-accent transition-colors"
                    >
                      {link.label}
                    </Link>
                  </li>
                ))}
              </ul>
            </div>
          ))}
        </div>

        <div className="mt-12 pt-8 border-t border-border/40 flex flex-col md:flex-row items-center justify-between gap-4">
          <p className="text-sm text-muted-foreground">
            © {new Date().getFullYear()} JobFlow. All rights reserved.
          </p>
          <p className="text-sm text-muted-foreground">
            Made with ❤️ for job seekers everywhere
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
