import axios from 'axios';
const api = axios.create({ baseURL: process.env.REACT_APP_API_BASE_URL || 'https://localhost:5001/api' });
export default api;
