import apiClient from './axiosConfig';

export const getCategories = () => apiClient.get('/categories');
export const getCategoryById = (id) => apiClient.get(`/categories/${id}`);
export const createCategory = (category) => apiClient.post('/categories', category);
export const updateCategory = (id, category) => apiClient.put(`/categories/${id}`, category);
export const deleteCategory = (id) => apiClient.delete(`/categories/${id}`);