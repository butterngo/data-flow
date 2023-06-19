import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://your-api.com",
  headers: {
    "Content-Type": "application/json",
  },
});

axiosInstance.interceptors.request.use(
  (config) => {
    // Add authentication headers
    config.headers["Authorization"] = `Bearer ${localStorage.getItem("token")}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    // Handle errors globally
    if (error.response.status === 401) {
      // Logout user
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
