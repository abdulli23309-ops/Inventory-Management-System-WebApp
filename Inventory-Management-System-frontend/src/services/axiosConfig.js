import axios from 'axios';

// We create a central instance. 
// If our backend port changes, we only update it in this one file, not in 50 different React components.
const apiClient = axios.create({
    baseURL: 'https://localhost:7207/api', // REPLACE with your actual Swagger/API port!
    headers: {
        'Content-Type': 'application/json'
    }
});

export default apiClient;