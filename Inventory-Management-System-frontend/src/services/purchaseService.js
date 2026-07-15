import apiClient from './axiosConfig';

export const getPurchases = () => apiClient.get('/purchases');
export const getPurchaseById = (id) => apiClient.get(`/purchases/${id}`);
export const createPurchase = (purchase) => apiClient.post('/purchases', purchase);
export const updatePurchase = (id, purchase) => apiClient.put(`/purchases/${id}`, purchase);
export const deletePurchase = (id) => apiClient.delete(`/purchases/${id}`);