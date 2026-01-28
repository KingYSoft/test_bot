import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/quotations',
      name: 'quotations',
      component: () => import('../pages/QuotationForm.vue')
    },
    {
      path: '/suppliers',
      name: 'suppliers',
      component: () => import('../views/SuppliersView.vue')
    },
    {
      path: '/reports',
      name: 'reports',
      component: () => import('../views/ReportsView.vue')
    }
  ]
})

export default router