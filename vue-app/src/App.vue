<template>
  <v-app>
    <v-app-bar color="primary" app dark>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>Vue 3 + Vuetify 3 + .NET 8.0 App</v-toolbar-title>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" temporary>
      <v-list nav>
        <v-list-item link to="/">
          <v-list-item-title>Home</v-list-item-title>
        </v-list-item>
        <v-list-item link to="/about">
          <v-list-item-title>About</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <router-view />
    </v-main>

    <v-footer class="d-flex flex-column">
      <div class="px-4 py-2 bg-grey-lighten-3 text-center w-100">
        {{ new Date().getFullYear() }} — <strong>Vue 3 + Vuetify 3 + .NET 8.0 App</strong>
      </div>
    </v-footer>
  </v-app>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const drawer = ref(false)
</script>

<style>
#app {
  font-family: Roboto, sans-serif;
}
</style>