<template>
  <v-app>
    <v-app-bar color="primary" dark>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-app-bar-title>空运海运费用报价系统</v-app-bar-title>
      
      <template #append>
        <v-btn icon="mdi-bell"></v-btn>
      </template>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" temporary>
      <v-list nav>
        <v-list-item to="/" title="首页" value="home" />
        <v-list-item to="/quotations" title="报价单管理" value="quotations" />
        <v-list-item to="/suppliers" title="供应商管理" value="suppliers" />
        <v-list-item to="/reports" title="报表统计" value="reports" />
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <router-view />
    </v-main>

    <v-footer app>
      <span>&copy; {{ new Date().getFullYear() }} 空运海运费用报价系统</span>
    </v-footer>
  </v-app>
</template>

<script setup>
import { ref } from 'vue'

const drawer = ref(null)
</script>