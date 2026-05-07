export const getToken = (): string | null => {
  return localStorage.getItem('token')
};

export const saveToken = (token: string): void => {
  localStorage.setItem('token', token)
};

export const removeToken = (): void => {
  localStorage.removeItem('token')
};

export const isLoggedIn = (): boolean => {
  return localStorage.getItem('token') !== null
};