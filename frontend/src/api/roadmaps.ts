import { apiClient } from './client';

export async function getRoadmap(id: number): Promise<any> {
  const response = await apiClient.get(`/roadmaps/${id}`);

  return response.data;
}

export async function getRoadmaps(): Promise<any[]> {
  const response = await apiClient.get('/roadmaps');

  return response.data;
}