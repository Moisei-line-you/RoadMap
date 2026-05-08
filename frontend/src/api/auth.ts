import { apiClient } from './client';

export async function register(
  username: string,
  email: string,
  password: string,
  confirmPassword: string
): Promise<void> {
  await apiClient.post('/auth/register', {
    username,
    email, 
    password,
    confirmPassword
  })
}

export async function login(
  username: string,
  password: string
): Promise<{ token: string }> {
  const response = await apiClient.post('/auth/login', {
    username,
    password
  })

  return response.data;
}