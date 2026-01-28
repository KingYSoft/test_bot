import { ref, Ref } from 'vue'
import axios, { AxiosResponse, AxiosError } from 'axios'

interface ApiResult<T> {
  data: Ref<T | null>
  loading: Ref<boolean>
  error: Ref<string | null>
  execute: () => Promise<void>
}

export function useApi<T>(
  url: string,
  method: 'GET' | 'POST' | 'PUT' | 'DELETE' = 'GET',
  body?: any
): ApiResult<T> {
  const data = ref<T | null>(null)
  const loading = ref<boolean>(false)
  const error = ref<string | null>(null)

  const execute = async (): Promise<void> => {
    loading.value = true
    error.value = null
    
    try {
      let response: AxiosResponse<T>
      
      switch (method) {
        case 'GET':
          response = await axios.get<T>(url)
          break
        case 'POST':
          response = await axios.post<T>(url, body)
          break
        case 'PUT':
          response = await axios.put<T>(url, body)
          break
        case 'DELETE':
          response = await axios.delete<T>(url)
          break
        default:
          throw new Error(`Unsupported method: ${method}`)
      }
      
      data.value = response.data
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'An unknown error occurred'
      error.value = errorMessage
      console.error('API call failed:', err)
    } finally {
      loading.value = false
    }
  }

  return {
    data,
    loading,
    error,
    execute
  }
}