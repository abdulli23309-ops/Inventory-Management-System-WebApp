import apiClient from './axiosConfig';

export const getSuppliers = () => apiClient.get('/suppliers');
export const getSupplierById = (id) => apiClient.get(`/suppliers/${id}`);
export const createSupplier = (supplier) => apiClient.post('/suppliers', supplier);
export const updateSupplier = (id, supplier) => apiClient.put(`/suppliers/${id}`, supplier);
export const deleteSupplier = (id) => apiClient.delete(`/suppliers/${id}`);