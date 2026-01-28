<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title>Welcome to Vue 3 + Vuetify 3 + .NET 8.0!</v-card-title>
          <v-card-subtitle>This is your home page</v-card-subtitle>
          <v-card-text>
            <p>You're running a modern web application using:</p>
            <ul>
              <li>Vue 3 with Composition API</li>
              <li>Vuetify 3 for Material Design components</li>
              <li>TypeScript for type safety</li>
              <li>.NET 8.0 for backend services</li>
            </ul>
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" @click="testApi">Test API Connection</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
    
    <v-row v-if="apiResponse">
      <v-col cols="12">
        <v-card>
          <v-card-title>API Response</v-card-title>
          <v-card-text>{{ apiResponse }}</v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios'

const apiResponse = ref<string | null>(null)

const testApi = async () => {
  try {
    const response = await axios.get('/api/test')
    apiResponse.value = JSON.stringify(response.data, null, 2)
  } catch (error) {
    console.error('Error calling API:', error)
    apiResponse.value = 'Error connecting to API'
  }
}
</script>