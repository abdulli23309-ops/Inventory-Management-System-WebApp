import apiClient from './axiosConfig';
export const getSales = () => apiClient.get('/sales');
export const getSaleById = (id) => apiClient.get(`/sales/${id}`);
export const createSale = (sale) => apiClient.post('/sales', sale);
export const updateSale = (id, sale) => apiClient.put(`/sales/${id}`, sale);
export const deleteSale = (id) => apiClient.delete(`/sales/${id}`);