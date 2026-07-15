import apiClient from './axiosConfig';

export const getProducts = () => apiClient.get('/products');
export const getProductById = (id) => apiClient.get(`/products/${id}`);
export const getProductsByCategory = (categoryId) => apiClient.get(`/products/category/${categoryId}`);
export const createProduct = (product) => apiClient.post('/products', product);
export const updateProduct = (id, product) => apiClient.put(`/products/${id}`, product);
export const deleteProduct = (id) => apiClient.delete(`/products/${id}`);