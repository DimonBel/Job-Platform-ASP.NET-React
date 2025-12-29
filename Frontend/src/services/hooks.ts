import { useQuery } from '@tanstack/react-query';
import { 
  jobsApi, 
  companiesApi, 
  categoriesApi, 
  statisticsApi, 
  tagsApi,
  JobSearchParams,
  CompanySearchParams 
} from './api';

// Jobs hooks
export function useJobs() {
  return useQuery({
    queryKey: ['jobs'],
    queryFn: jobsApi.getAll,
  });
}

export function useFeaturedJobs(count = 4) {
  return useQuery({
    queryKey: ['jobs', 'featured', count],
    queryFn: () => jobsApi.getFeatured(count),
  });
}

export function useJob(id: number | string) {
  return useQuery({
    queryKey: ['jobs', id],
    queryFn: () => jobsApi.getById(id),
    enabled: !!id,
  });
}

export function useJobDetails(id: number | string) {
  return useQuery({
    queryKey: ['jobs', id, 'details'],
    queryFn: () => jobsApi.getDetails(id),
    enabled: !!id,
  });
}

export function useJobSearch(params: JobSearchParams) {
  return useQuery({
    queryKey: ['jobs', 'search', params],
    queryFn: () => jobsApi.search(params),
  });
}

// Companies hooks
export function useCompanies() {
  return useQuery({
    queryKey: ['companies'],
    queryFn: companiesApi.getAll,
  });
}

export function useCompanySearch(params: CompanySearchParams) {
  return useQuery({
    queryKey: ['companies', 'search', params],
    queryFn: () => companiesApi.search(params),
  });
}

export function useTopCompanies(count = 6) {
  return useQuery({
    queryKey: ['companies', 'top', count],
    queryFn: () => companiesApi.getTop(count),
  });
}

export function useCompany(id: number | string) {
  return useQuery({
    queryKey: ['companies', id],
    queryFn: () => companiesApi.getById(id),
    enabled: !!id,
  });
}

export function useCompanyDetails(id: number | string) {
  return useQuery({
    queryKey: ['companies', id, 'details'],
    queryFn: () => companiesApi.getDetails(id),
    enabled: !!id,
  });
}

// Categories hooks
export function useCategories() {
  return useQuery({
    queryKey: ['categories'],
    queryFn: categoriesApi.getAll,
  });
}

// Statistics hooks
export function useStatistics() {
  return useQuery({
    queryKey: ['statistics'],
    queryFn: statisticsApi.get,
  });
}

// Tags hooks
export function useTags() {
  return useQuery({
    queryKey: ['tags'],
    queryFn: tagsApi.getAll,
  });
}
