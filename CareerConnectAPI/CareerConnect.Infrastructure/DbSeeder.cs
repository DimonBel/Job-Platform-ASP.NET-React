using CareerConnect.Domain.Entities;
using CareerConnect.Infrastructure.Data;

namespace CareerConnect.Infrastructure;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Jobs.AnyAsync())
            return;

        // Seed Categories
        var categories = new List<Category>
        {
            new() { Name = "Technology", Description = "Software, IT, and tech jobs", Icon = "Code2", Color = "bg-blue-500/10 text-blue-600" },
            new() { Name = "Design", Description = "UI/UX, graphic design jobs", Icon = "Palette", Color = "bg-pink-500/10 text-pink-600" },
            new() { Name = "Marketing", Description = "Digital marketing, SEO jobs", Icon = "Megaphone", Color = "bg-orange-500/10 text-orange-600" },
            new() { Name = "Sales", Description = "Sales and business development", Icon = "TrendingUp", Color = "bg-green-500/10 text-green-600" },
            new() { Name = "Finance", Description = "Accounting and finance jobs", Icon = "TrendingUp", Color = "bg-emerald-500/10 text-emerald-600" },
            new() { Name = "Healthcare", Description = "Medical and healthcare jobs", Icon = "Stethoscope", Color = "bg-red-500/10 text-red-600" },
            new() { Name = "Education", Description = "Teaching and training jobs", Icon = "GraduationCap", Color = "bg-purple-500/10 text-purple-600" },
            new() { Name = "Real Estate", Description = "Property and real estate jobs", Icon = "Building2", Color = "bg-teal-500/10 text-teal-600" },
            new() { Name = "Logistics", Description = "Supply chain and logistics", Icon = "Truck", Color = "bg-amber-500/10 text-amber-600" },
            new() { Name = "Human Resources", Description = "HR and recruitment jobs", Icon = "Users", Color = "bg-indigo-500/10 text-indigo-600" }
        };
        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // Seed Tags
        var tags = new List<Tag>
        {
            new() { Name = "React" }, new() { Name = "TypeScript" }, new() { Name = "Node.js" },
            new() { Name = "Python" }, new() { Name = "AWS" }, new() { Name = "Docker" },
            new() { Name = "Kubernetes" }, new() { Name = "PostgreSQL" }, new() { Name = "MongoDB" },
            new() { Name = "GraphQL" }, new() { Name = "REST API" }, new() { Name = "Figma" },
            new() { Name = "Adobe XD" }, new() { Name = "UI/UX" }, new() { Name = "SEO" },
            new() { Name = "Google Analytics" }, new() { Name = "Machine Learning" }, new() { Name = "AI" },
            new() { Name = "Java" }, new() { Name = "C#" }, new() { Name = ".NET" },
            new() { Name = "Angular" }, new() { Name = "Vue.js" }, new() { Name = "Next.js" },
            new() { Name = "Tailwind CSS" }, new() { Name = "SQL" }, new() { Name = "Azure" },
            new() { Name = "GCP" }, new() { Name = "Agile" }, new() { Name = "Scrum" },
            new() { Name = "Git" }, new() { Name = "CI/CD" }, new() { Name = "Microservices" },
            new() { Name = "Leadership" }, new() { Name = "Communication" }, new() { Name = "Problem Solving" }
        };
        await context.Tags.AddRangeAsync(tags);
        await context.SaveChangesAsync();

        // Seed Companies
        var companies = new List<Company>
        {
            new() { Name = "TechCorp Inc.", Description = "Leading technology company focused on innovative solutions. We build cutting-edge software that powers businesses worldwide.", Industry = "Technology", Location = "San Francisco, CA", Size = "1000-5000", Rating = 4.5m, ReviewCount = 234, IsVerified = true, Website = "https://techcorp.com", Logo = "https://ui-avatars.com/api/?name=TechCorp&background=3b82f6&color=fff&size=128" },
            new() { Name = "DesignStudio", Description = "Award-winning design agency creating beautiful experiences. Our team of creative professionals delivers exceptional design solutions.", Industry = "Design", Location = "New York, NY", Size = "100-500", Rating = 4.8m, ReviewCount = 156, IsVerified = true, Website = "https://designstudio.com", Logo = "https://ui-avatars.com/api/?name=DesignStudio&background=ec4899&color=fff&size=128" },
            new() { Name = "StartupXYZ", Description = "Fast-growing startup disrupting the fintech industry. Join us in revolutionizing how people manage their finances.", Industry = "FinTech", Location = "Austin, TX", Size = "50-100", Rating = 4.2m, ReviewCount = 89, IsVerified = true, Website = "https://startupxyz.com", Logo = "https://ui-avatars.com/api/?name=StartupXYZ&background=10b981&color=fff&size=128" },
            new() { Name = "CloudFirst", Description = "Enterprise cloud solutions for modern businesses. We help companies migrate and thrive in the cloud.", Industry = "Cloud Computing", Location = "Seattle, WA", Size = "500-1000", Rating = 4.6m, ReviewCount = 312, IsVerified = true, Website = "https://cloudfirst.com", Logo = "https://ui-avatars.com/api/?name=CloudFirst&background=6366f1&color=fff&size=128" },
            new() { Name = "DataDriven Co.", Description = "Transforming data into actionable insights. Our analytics platform helps businesses make smarter decisions.", Industry = "Data Analytics", Location = "Boston, MA", Size = "200-500", Rating = 4.4m, ReviewCount = 178, IsVerified = true, Website = "https://datadriven.co", Logo = "https://ui-avatars.com/api/?name=DataDriven&background=8b5cf6&color=fff&size=128" },
            new() { Name = "NeuralTech AI", Description = "Pioneering AI solutions for enterprise applications. We're building the future of artificial intelligence.", Industry = "Artificial Intelligence", Location = "Remote", Size = "100-200", Rating = 4.7m, ReviewCount = 145, IsVerified = true, Website = "https://neuraltech.ai", Logo = "https://ui-avatars.com/api/?name=NeuralTech&background=f59e0b&color=fff&size=128" },
            new() { Name = "HealthPlus", Description = "Digital health solutions improving patient outcomes. Our platform connects patients with healthcare providers seamlessly.", Industry = "Healthcare", Location = "Chicago, IL", Size = "500-1000", Rating = 4.3m, ReviewCount = 267, IsVerified = true, Website = "https://healthplus.com", Logo = "https://ui-avatars.com/api/?name=HealthPlus&background=ef4444&color=fff&size=128" },
            new() { Name = "EduTech Global", Description = "Transforming education through technology. We make learning accessible to everyone, everywhere.", Industry = "Education", Location = "Los Angeles, CA", Size = "200-500", Rating = 4.5m, ReviewCount = 189, IsVerified = true, Website = "https://edutech.global", Logo = "https://ui-avatars.com/api/?name=EduTech&background=a855f7&color=fff&size=128" },
            new() { Name = "GreenEnergy Corp", Description = "Sustainable energy solutions for a better tomorrow. Leading the transition to renewable energy.", Industry = "Energy", Location = "Denver, CO", Size = "1000-5000", Rating = 4.4m, ReviewCount = 298, IsVerified = true, Website = "https://greenenergy.com", Logo = "https://ui-avatars.com/api/?name=GreenEnergy&background=22c55e&color=fff&size=128" },
            new() { Name = "SecureNet", Description = "Cybersecurity solutions protecting businesses worldwide. Your security is our priority.", Industry = "Cybersecurity", Location = "Washington, DC", Size = "500-1000", Rating = 4.6m, ReviewCount = 203, IsVerified = true, Website = "https://securenet.com", Logo = "https://ui-avatars.com/api/?name=SecureNet&background=64748b&color=fff&size=128" },
            new() { Name = "MediaWorks", Description = "Creative media and entertainment company. We produce content that inspires and entertains millions.", Industry = "Media & Entertainment", Location = "Miami, FL", Size = "200-500", Rating = 4.2m, ReviewCount = 134, IsVerified = true, Website = "https://mediaworks.com", Logo = "https://ui-avatars.com/api/?name=MediaWorks&background=f43f5e&color=fff&size=128" },
            new() { Name = "LogiFlow", Description = "Smart logistics and supply chain solutions. Optimizing the way goods move around the world.", Industry = "Logistics", Location = "Atlanta, GA", Size = "1000-5000", Rating = 4.1m, ReviewCount = 276, IsVerified = true, Website = "https://logiflow.com", Logo = "https://ui-avatars.com/api/?name=LogiFlow&background=0ea5e9&color=fff&size=128" }
        };
        await context.Companies.AddRangeAsync(companies);
        await context.SaveChangesAsync();

        // Get categories and tags for jobs
        var techCategory = categories.First(c => c.Name == "Technology");
        var designCategory = categories.First(c => c.Name == "Design");
        var marketingCategory = categories.First(c => c.Name == "Marketing");
        var financeCategory = categories.First(c => c.Name == "Finance");
        var healthcareCategory = categories.First(c => c.Name == "Healthcare");
        var educationCategory = categories.First(c => c.Name == "Education");
        var salesCategory = categories.First(c => c.Name == "Sales");
        var hrCategory = categories.First(c => c.Name == "Human Resources");

        var tagDict = tags.ToDictionary(t => t.Name, t => t);

        var jobs = new List<Job>
        {
            // Technology Jobs
            CreateJob("Senior Frontend Developer", "We're looking for a talented Senior Frontend Developer to join our growing team. You'll work on cutting-edge projects using React and TypeScript, building responsive and performant web applications.", "San Francisco, CA", "$120,000 - $160,000", "Full-time", "Senior", true, true, -2, companies[0].Id, techCategory.Id, new[] { "React", "TypeScript", "Next.js", "Tailwind CSS" }, tagDict,
                new[] { "Design and implement scalable frontend solutions", "Collaborate with cross-functional teams", "Mentor junior developers", "Participate in code reviews", "Optimize application performance" },
                new[] { "5+ years of frontend development experience", "Strong proficiency in React and TypeScript", "Experience with state management (Redux, Zustand)", "Knowledge of modern CSS frameworks" },
                new[] { "Competitive salary and equity", "Comprehensive health insurance", "Flexible work arrangements", "Professional development budget", "401k matching" }),

            CreateJob("Full Stack Engineer", "Join our engineering team to build innovative products that impact millions of users. You'll work across the entire stack.", "Remote", "$100,000 - $140,000", "Contract", "Mid Level", true, true, -5, companies[2].Id, techCategory.Id, new[] { "Node.js", "React", "AWS", "PostgreSQL" }, tagDict,
                new[] { "Build and maintain full-stack applications", "Write clean, maintainable code", "Design database schemas", "Implement RESTful APIs" },
                new[] { "3+ years of full-stack development", "Experience with Node.js and React", "Database design skills", "Cloud platform experience" },
                new[] { "Remote-first culture", "Stock options", "Unlimited PTO", "Home office stipend" }),

            CreateJob("Backend Engineer - Python", "Build robust backend systems using Python and modern frameworks. Join our data platform team.", "Boston, MA", "$110,000 - $150,000", "Part-time", "Mid Level", true, true, -3, companies[4].Id, techCategory.Id, new[] { "Python", "PostgreSQL", "Docker", "AWS" }, tagDict,
                new[] { "Develop and maintain Python microservices", "Design and optimize database queries", "Build data processing pipelines", "Write comprehensive tests" },
                new[] { "3+ years Python development", "Experience with FastAPI or Django", "Strong SQL skills", "Docker and containerization" },
                new[] { "Competitive compensation", "Learning budget", "Flexible hours", "Health and dental" }),

            CreateJob("DevOps Engineer", "Build and maintain our cloud infrastructure and CI/CD pipelines. Help us scale to millions of users.", "Seattle, WA", "$130,000 - $170,000", "Full-time", "Senior", true, true, -7, companies[3].Id, techCategory.Id, new[] { "AWS", "Kubernetes", "Docker", "CI/CD" }, tagDict,
                new[] { "Manage AWS infrastructure", "Implement CI/CD pipelines", "Monitor system performance", "Automate deployment processes" },
                new[] { "5+ years of DevOps experience", "Strong AWS knowledge", "Kubernetes expertise", "Infrastructure as Code" },
                new[] { "AWS certification sponsorship", "Competitive salary", "Stock options", "Conference attendance" }),

            CreateJob("Machine Learning Engineer", "Join our AI team to build and deploy machine learning models at scale. Work on cutting-edge problems.", "Remote", "$140,000 - $190,000", "Full-time", "Senior", true, true, -1, companies[5].Id, techCategory.Id, new[] { "Python", "Machine Learning", "AI", "AWS" }, tagDict,
                new[] { "Design and implement ML models", "Build data pipelines for model training", "Deploy models to production", "Collaborate with research team" },
                new[] { "MS/PhD in Computer Science or related field", "Experience with TensorFlow/PyTorch", "Strong Python skills", "ML production experience" },
                new[] { "Cutting-edge projects", "Research time", "Publication support", "Top-tier compensation" }),

            CreateJob("iOS Developer", "Create beautiful, performant iOS applications used by millions. Join our mobile team.", "New York, NY", "$115,000 - $155,000", "Contract", "Mid Level", false, true, -4, companies[0].Id, techCategory.Id, new[] { "React", "TypeScript" }, tagDict,
                new[] { "Develop iOS applications using Swift", "Implement new features", "Optimize app performance", "Work with design team" },
                new[] { "3+ years iOS development", "Swift and SwiftUI proficiency", "App Store publishing experience", "Unit testing skills" },
                new[] { "Latest Apple hardware", "App Store features", "Great team culture", "Health benefits" }),

            CreateJob("Android Developer", "Build Android applications that delight users. Work with the latest Android technologies.", "Austin, TX", "$110,000 - $150,000", "Freelance", "Mid Level", false, true, -6, companies[2].Id, techCategory.Id, new[] { "Java" }, tagDict,
                new[] { "Develop Android applications", "Write clean Kotlin code", "Implement Material Design", "Performance optimization" },
                new[] { "3+ years Android development", "Kotlin expertise", "Jetpack Compose knowledge", "Testing experience" },
                new[] { "Startup equity", "Flexible schedule", "Learning opportunities", "Team events" }),

            CreateJob("Software Engineer - Java", "Join our enterprise team building robust Java applications for Fortune 500 clients.", "Chicago, IL", "$100,000 - $140,000", "Part-time", "Mid Level", false, true, -8, companies[6].Id, techCategory.Id, new[] { "Java", ".NET", "SQL" }, tagDict,
                new[] { "Develop Java applications", "Design system architecture", "Code reviews", "Technical documentation" },
                new[] { "3+ years Java experience", "Spring Boot knowledge", "Microservices architecture", "Agile experience" },
                new[] { "Enterprise clients", "Stable environment", "Growth path", "Benefits package" }),

            CreateJob("Security Engineer", "Protect our systems and data. Join our world-class security team.", "Washington, DC", "$125,000 - $165,000", "Full-time", "Senior", true, true, -2, companies[9].Id, techCategory.Id, new[] { "Python", "AWS", "Docker" }, tagDict,
                new[] { "Conduct security assessments", "Implement security controls", "Incident response", "Security training" },
                new[] { "5+ years security experience", "Security certifications (CISSP, etc.)", "Penetration testing skills", "Cloud security knowledge" },
                new[] { "Important mission", "Cert sponsorship", "Competitive pay", "Government contracts" }),

            CreateJob("Data Engineer", "Build data infrastructure that powers business decisions. Work with petabytes of data.", "San Francisco, CA", "$120,000 - $160,000", "Contract", "Mid Level", false, true, -5, companies[4].Id, techCategory.Id, new[] { "Python", "SQL", "AWS", "Docker" }, tagDict,
                new[] { "Build data pipelines", "Design data warehouse", "ETL processes", "Data quality monitoring" },
                new[] { "3+ years data engineering", "SQL expertise", "Spark/Airflow experience", "Cloud platforms" },
                new[] { "Data-driven culture", "Latest tools", "Smart colleagues", "Good compensation" }),

            // Design Jobs
            CreateJob("UI/UX Designer", "Create beautiful, intuitive designs for web and mobile applications. Join our award-winning design team.", "New York, NY", "$90,000 - $130,000", "Full-time", "Mid Level", true, true, -3, companies[1].Id, designCategory.Id, new[] { "Figma", "UI/UX", "Adobe XD" }, tagDict,
                new[] { "Create wireframes and prototypes", "Conduct user research", "Design system maintenance", "Collaborate with developers" },
                new[] { "3+ years of UI/UX design experience", "Proficiency in Figma", "Portfolio required", "User research skills" },
                new[] { "Creative work environment", "Annual design conference", "Latest design tools", "Flexible hours" }),

            CreateJob("Senior Product Designer", "Lead design initiatives for our flagship products. Shape the future of our user experience.", "San Francisco, CA", "$130,000 - $170,000", "Full-time", "Senior", true, true, -1, companies[1].Id, designCategory.Id, new[] { "Figma", "UI/UX" }, tagDict,
                new[] { "Lead product design initiatives", "Mentor junior designers", "Define design strategy", "User testing" },
                new[] { "5+ years product design", "Leadership experience", "Strong portfolio", "Design systems expertise" },
                new[] { "Design leadership role", "High impact work", "Great compensation", "Creative freedom" }),

            CreateJob("Brand Designer", "Define and evolve our visual identity. Create stunning brand assets across all channels.", "Los Angeles, CA", "$80,000 - $110,000", "Freelance", "Mid Level", false, true, -9, companies[10].Id, designCategory.Id, new[] { "Adobe XD", "Figma" }, tagDict,
                new[] { "Create brand guidelines", "Design marketing materials", "Social media graphics", "Video graphics" },
                new[] { "3+ years brand design", "Adobe Creative Suite", "Motion graphics a plus", "Strong portfolio" },
                new[] { "Creative industry", "Exciting projects", "Collaborative team", "Benefits package" }),

            CreateJob("Motion Designer", "Create engaging animations and motion graphics for digital products and marketing.", "Remote", "$85,000 - $120,000", "Contract", "Mid Level", false, true, -4, companies[10].Id, designCategory.Id, new[] { "Figma", "UI/UX" }, tagDict,
                new[] { "Create animations for products", "Video editing", "Design animated ads", "Collaborate with marketing" },
                new[] { "After Effects expertise", "3+ years motion design", "Video editing skills", "Creative portfolio" },
                new[] { "Remote work", "Creative projects", "Flexible schedule", "Equipment provided" }),

            // Marketing Jobs
            CreateJob("Digital Marketing Manager", "Lead our digital marketing strategy across all channels. Drive growth and brand awareness.", "New York, NY", "$90,000 - $130,000", "Full-time", "Senior", true, true, -2, companies[0].Id, marketingCategory.Id, new[] { "SEO", "Google Analytics" }, tagDict,
                new[] { "Develop marketing strategy", "Manage paid campaigns", "SEO optimization", "Analytics and reporting" },
                new[] { "5+ years digital marketing", "Google Ads certification", "Analytics expertise", "Team management" },
                new[] { "Marketing budget", "Growth opportunity", "Great team", "Competitive salary" }),

            CreateJob("Content Marketing Specialist", "Create compelling content that drives engagement and conversions. Tell our brand story.", "Remote", "$65,000 - $90,000", "Part-time", "Mid Level", false, true, -6, companies[7].Id, marketingCategory.Id, new[] { "SEO", "Google Analytics" }, tagDict,
                new[] { "Write blog posts and articles", "Social media content", "Email campaigns", "Content strategy" },
                new[] { "3+ years content marketing", "Strong writing skills", "SEO knowledge", "Social media experience" },
                new[] { "Remote work", "Creative freedom", "Learning budget", "Flexible hours" }),

            CreateJob("SEO Specialist", "Optimize our web presence for search engines. Drive organic traffic and improve rankings.", "Boston, MA", "$70,000 - $100,000", "Freelance", "Mid Level", false, true, -8, companies[4].Id, marketingCategory.Id, new[] { "SEO", "Google Analytics" }, tagDict,
                new[] { "Technical SEO audits", "Keyword research", "Link building", "Performance tracking" },
                new[] { "3+ years SEO experience", "Technical SEO skills", "Analytics tools", "Content optimization" },
                new[] { "Data-driven team", "Growth company", "Good benefits", "Development opportunities" }),

            // Finance Jobs
            CreateJob("Financial Analyst", "Analyze financial data and provide insights to drive business decisions. Join our finance team.", "Chicago, IL", "$75,000 - $100,000", "Contract", "Mid Level", false, true, -5, companies[6].Id, financeCategory.Id, new[] { "SQL", "Python" }, tagDict,
                new[] { "Financial modeling", "Budget analysis", "Reporting", "Forecasting" },
                new[] { "3+ years financial analysis", "Excel expertise", "SQL skills preferred", "CFA a plus" },
                new[] { "Growth path", "Bonus structure", "Health benefits", "401k match" }),

            CreateJob("Senior Accountant", "Manage accounting operations and ensure financial accuracy. Lead month-end close process.", "Denver, CO", "$80,000 - $110,000", "Full-time", "Senior", false, true, -7, companies[8].Id, financeCategory.Id, new[] { "SQL" }, tagDict,
                new[] { "Month-end close", "Financial reporting", "Audit support", "Process improvement" },
                new[] { "CPA required", "5+ years accounting", "ERP experience", "GAAP knowledge" },
                new[] { "Stable company", "Work-life balance", "Professional development", "Good benefits" }),

            // Sales Jobs
            CreateJob("Account Executive", "Drive revenue growth by acquiring new customers and expanding existing accounts.", "Austin, TX", "$70,000 - $120,000 + Commission", "Full-time", "Mid Level", true, true, -3, companies[2].Id, salesCategory.Id, new[] { "Communication", "Leadership" }, tagDict,
                new[] { "Prospect new clients", "Manage sales pipeline", "Close deals", "Client relationships" },
                new[] { "3+ years B2B sales", "SaaS experience preferred", "CRM proficiency", "Strong communication" },
                new[] { "Uncapped commission", "President's Club trips", "Fast growth", "Great culture" }),

            CreateJob("Sales Development Representative", "Generate qualified leads and book meetings for our sales team. Start your sales career.", "Remote", "$50,000 - $70,000 + Bonus", "Internship", "Entry Level", false, true, -4, companies[2].Id, salesCategory.Id, new[] { "Communication" }, tagDict,
                new[] { "Cold outreach", "Lead qualification", "Book meetings", "CRM updates" },
                new[] { "1+ years in sales or customer service", "Strong communication", "Coachable attitude", "Tech-savvy" },
                new[] { "Career path to AE", "Training provided", "Remote work", "Team environment" }),

            // Healthcare Jobs
            CreateJob("Healthcare Data Analyst", "Analyze healthcare data to improve patient outcomes. Work with clinical teams.", "Chicago, IL", "$85,000 - $115,000", "Part-time", "Mid Level", false, true, -6, companies[6].Id, healthcareCategory.Id, new[] { "Python", "SQL" }, tagDict,
                new[] { "Analyze clinical data", "Create dashboards", "Support research", "Data quality" },
                new[] { "Healthcare analytics experience", "SQL proficiency", "HIPAA knowledge", "Statistics background" },
                new[] { "Meaningful work", "Health benefits", "Research opportunities", "Stable employer" }),

            CreateJob("Product Manager - Healthcare", "Lead product development for our healthcare platform. Improve patient care through technology.", "Remote", "$120,000 - $160,000", "Full-time", "Senior", true, true, -2, companies[6].Id, healthcareCategory.Id, new[] { "Agile", "Leadership" }, tagDict,
                new[] { "Define product roadmap", "Work with engineering", "User research", "Stakeholder management" },
                new[] { "5+ years product management", "Healthcare experience", "Technical background", "Strong communication" },
                new[] { "Impact healthcare", "Remote work", "Great compensation", "Mission-driven" }),

            // Education Jobs
            CreateJob("Curriculum Developer", "Create engaging educational content for our learning platform. Shape how millions learn.", "Los Angeles, CA", "$70,000 - $95,000", "Contract", "Mid Level", false, true, -5, companies[7].Id, educationCategory.Id, new[] { "Communication" }, tagDict,
                new[] { "Develop course content", "Create assessments", "Work with instructors", "Content updates" },
                new[] { "Education background", "Instructional design", "Writing skills", "EdTech experience" },
                new[] { "Impact education", "Creative work", "Good benefits", "Learning culture" }),

            CreateJob("Senior Instructional Designer", "Design learning experiences that engage and educate. Lead instructional design initiatives.", "Remote", "$85,000 - $115,000", "Full-time", "Senior", false, true, -3, companies[7].Id, educationCategory.Id, new[] { "Communication", "Leadership" }, tagDict,
                new[] { "Lead ID projects", "Mentor team", "Learning strategy", "Technology integration" },
                new[] { "5+ years instructional design", "LMS experience", "Multimedia skills", "Leadership experience" },
                new[] { "Remote work", "Meaningful impact", "Growth opportunities", "Collaborative team" }),

            // HR Jobs
            CreateJob("Technical Recruiter", "Source and hire top tech talent. Partner with hiring managers to build amazing teams.", "San Francisco, CA", "$80,000 - $120,000", "Contract", "Mid Level", false, true, -4, companies[0].Id, hrCategory.Id, new[] { "Communication", "Leadership" }, tagDict,
                new[] { "Source candidates", "Conduct interviews", "Manage pipeline", "Hiring partnerships" },
                new[] { "3+ years tech recruiting", "ATS experience", "Technical understanding", "Strong network" },
                new[] { "Fast-paced environment", "Bonus structure", "Great team", "Career growth" }),

            CreateJob("HR Business Partner", "Partner with business leaders to drive HR initiatives and support organizational goals.", "Seattle, WA", "$95,000 - $130,000", "Full-time", "Senior", false, true, -6, companies[3].Id, hrCategory.Id, new[] { "Leadership", "Communication" }, tagDict,
                new[] { "HR strategy", "Employee relations", "Performance management", "Change management" },
                new[] { "5+ years HR experience", "HRBP experience", "Employment law knowledge", "Strong communication" },
                new[] { "Strategic role", "Growing company", "Great benefits", "Development opportunities" }),

            // More Tech Jobs for variety
            CreateJob("Site Reliability Engineer", "Ensure our systems are reliable, scalable, and performant. Build the infrastructure of the future.", "Seattle, WA", "$140,000 - $180,000", "Full-time", "Senior", true, true, -1, companies[3].Id, techCategory.Id, new[] { "AWS", "Kubernetes", "Python", "Docker" }, tagDict,
                new[] { "Maintain system reliability", "Incident response", "Automation", "Capacity planning" },
                new[] { "5+ years SRE/DevOps", "Strong coding skills", "Cloud expertise", "On-call experience" },
                new[] { "Top compensation", "Interesting problems", "Great team", "Work-life balance" }),

            CreateJob("Engineering Manager", "Lead a team of talented engineers. Drive technical excellence and team growth.", "San Francisco, CA", "$180,000 - $230,000", "Full-time", "Lead", true, true, -2, companies[0].Id, techCategory.Id, new[] { "Leadership", "Agile", "Scrum" }, tagDict,
                new[] { "Lead engineering team", "Technical direction", "Hiring and development", "Cross-team collaboration" },
                new[] { "5+ years engineering experience", "2+ years management", "Strong technical background", "Leadership skills" },
                new[] { "Leadership role", "High impact", "Excellent compensation", "Equity package" }),

            CreateJob("Junior Frontend Developer", "Start your career building web applications with React. Great opportunity to learn and grow.", "Remote", "$60,000 - $80,000", "Internship", "Entry Level", false, true, -3, companies[2].Id, techCategory.Id, new[] { "React", "TypeScript", "Git" }, tagDict,
                new[] { "Build UI components", "Fix bugs", "Write tests", "Code reviews" },
                new[] { "CS degree or bootcamp", "JavaScript knowledge", "React basics", "Eagerness to learn" },
                new[] { "Mentorship program", "Learning budget", "Remote work", "Growth path" }),

            CreateJob("QA Engineer", "Ensure product quality through comprehensive testing. Build test automation frameworks.", "Austin, TX", "$85,000 - $115,000", "Part-time", "Mid Level", false, true, -5, companies[2].Id, techCategory.Id, new[] { "Python", "Git" }, tagDict,
                new[] { "Create test plans", "Automation testing", "Bug tracking", "Quality processes" },
                new[] { "3+ years QA experience", "Test automation", "Selenium/Cypress", "API testing" },
                new[] { "Quality-focused culture", "Modern tools", "Growth opportunity", "Good benefits" }),

            CreateJob("Technical Writer", "Create clear, comprehensive documentation for our products and APIs.", "Remote", "$75,000 - $100,000", "Freelance", "Mid Level", false, true, -7, companies[3].Id, techCategory.Id, new[] { "Communication", "Git" }, tagDict,
                new[] { "Write documentation", "API docs", "User guides", "Release notes" },
                new[] { "3+ years technical writing", "API documentation", "Developer audience", "Clear communication" },
                new[] { "Remote work", "Great product", "Flexible hours", "Learning opportunities" }),

            CreateJob("VP of Engineering", "Lead our entire engineering organization. Set technical strategy and build world-class teams.", "San Francisco, CA", "$280,000 - $350,000", "Full-time", "Executive", true, true, -1, companies[0].Id, techCategory.Id, new[] { "Leadership", "Agile" }, tagDict,
                new[] { "Lead engineering org", "Technical strategy", "Executive leadership", "Hiring and scaling" },
                new[] { "10+ years engineering", "5+ years leadership", "Scaled teams", "Strategic thinking" },
                new[] { "Executive role", "Board interaction", "Significant equity", "High impact" })
        };

        await context.Jobs.AddRangeAsync(jobs);
        await context.SaveChangesAsync();

        // Add some sample users
        var users = new List<User>
        {
            new() { Email = "john.doe@email.com", PasswordHash = "hashed", FirstName = "John", LastName = "Doe", Role = "JobSeeker", IsActive = true },
            new() { Email = "jane.smith@email.com", PasswordHash = "hashed", FirstName = "Jane", LastName = "Smith", Role = "JobSeeker", IsActive = true },
            new() { Email = "employer@techcorp.com", PasswordHash = "hashed", FirstName = "Tech", LastName = "Employer", Role = "Employer", IsActive = true, CompanyId = companies[0].Id }
        };
        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }

    private static Job CreateJob(
        string title, string description, string location, string salary,
        string jobType, string experienceLevel, bool isFeatured, bool isActive,
        int daysAgo, int companyId, int categoryId,
        string[] tagNames, Dictionary<string, Tag> tagDict,
        string[] responsibilities, string[] requirements, string[] benefits)
    {
        var job = new Job
        {
            Title = title,
            Description = description,
            Location = location,
            Salary = salary,
            JobType = jobType,
            ExperienceLevel = experienceLevel,
            IsFeatured = isFeatured,
            IsActive = isActive,
            PostedDate = DateTime.UtcNow.AddDays(daysAgo),
            CompanyId = companyId,
            CategoryId = categoryId,
            JobTags = tagNames.Where(t => tagDict.ContainsKey(t)).Select(t => new JobTag { TagId = tagDict[t].Id }).ToList(),
            Responsibilities = responsibilities.Select((r, i) => new JobResponsibility { Description = r, Order = i }).ToList(),
            Requirements = requirements.Select((r, i) => new JobRequirement { Description = r, Order = i }).ToList(),
            Benefits = benefits.Select((b, i) => new JobBenefit { Description = b, Order = i }).ToList()
        };
        return job;
    }

    private static Task<bool> AnyAsync(this Microsoft.EntityFrameworkCore.DbSet<Job> jobs)
    {
        return Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.AnyAsync(jobs);
    }
}
