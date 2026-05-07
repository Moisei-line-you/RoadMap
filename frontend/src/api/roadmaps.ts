import { apiClient } from './client';

export async function getRoadmap(id: number): Promise<any> {
  const response = await apiClient.get(`/api/roadmaps/${id}`)

  return response.data
}