import { apiClient } from './client';

export async function getProgress(roadmapId: number): Promise<any> {
    const response = await apiClient.get(`/api/roadmaps/${roadmapId}/progress`)

    return response.data
}

export async function markNodeComplete(roadmapId: number, nodeId: number): Promise<void> {
  await apiClient.post(`/api/roadmaps/${roadmapId}/progress/nodes/${nodeId}`)
}

export async function unmarkNodeComplete(roadmapId: number, nodeId: number): Promise<void> {
  await apiClient.delete(`/api/roadmaps/${roadmapId}/progress/nodes/${nodeId}`)
}