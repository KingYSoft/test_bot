<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title class="d-flex justify-space-between align-center">
            <span>供应商管理</span>
            <v-btn color="primary" prepend-icon="mdi-plus">添加供应商</v-btn>
          </v-card-title>
          
          <v-card-text>
            <v-data-table
              :headers="headers"
              :items="suppliers"
              :items-per-page="10"
              class="elevation-1"
            >
              <template #item.actions="{ item }">
                <v-btn
                  color="primary"
                  size="small"
                  variant="text"
                  @click="editSupplier(item)"
                >
                  编辑
                </v-btn>
                <v-btn
                  color="error"
                  size="small"
                  variant="text"
                  @click="deleteSupplier(item)"
                >
                  删除
                </v-btn>
              </template>
            </v-data-table>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useApi } from '@/composables/useApi'

const { get } = useApi()

const headers = [
  { title: 'ID', key: 'id' },
  { title: '名称', key: 'name' },
  { title: '联系人', key: 'contactPerson' },
  { title: '邮箱', key: 'email' },
  { title: '电话', key: 'phone' },
  { title: '操作', key: 'actions', sortable: false }
]

const suppliers = ref([])

onMounted(async () => {
  try {
    const response = await get('/api/suppliers')
    suppliers.value = response.data
  } catch (error) {
    console.error('获取供应商列表失败:', error)
  }
})

const editSupplier = (supplier) => {
  console.log('编辑供应商:', supplier)
}

const deleteSupplier = (supplier) => {
  console.log('删除供应商:', supplier)
}
</script>