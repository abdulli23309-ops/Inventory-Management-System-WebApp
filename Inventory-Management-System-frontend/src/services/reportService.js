import apiClient from './axiosConfig';

export const getInventoryValuation = () => apiClient.get('/reports/inventory-valuation');