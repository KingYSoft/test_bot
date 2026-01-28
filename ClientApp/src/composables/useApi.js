import axios from 'axios'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api'

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json'
  }
})

export const useApi = () => {
  const get = async (url, config = {}) => {
    return await apiClient.get(url, config)
  }

  const post = async (url, data, config = {}) => {
    return await apiClient.post(url, data, config)
  }

  const put = async (url, data, config = {}) => {
    return await apiClient.put(url, data, config)
  }

  const del = async (url, config = {}) => {
    return await apiClient.delete(url, config)
  }

  return {
    get,
    post,
    put,
    del
  }
}