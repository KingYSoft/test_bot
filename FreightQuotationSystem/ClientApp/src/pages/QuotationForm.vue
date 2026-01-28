<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1 class="text-h4 mb-6">创建报价单</h1>
        
        <v-form @submit.prevent="submitQuotation">
          <v-card>
            <v-card-title>基本信息</v-card-title>
            <v-card-text>
              <v-row>
                <v-col cols="12" md="6">
                  <SupplierSelector 
                    v-model:selectedSupplier="formData.supplierId"
                    :model-value="formData.supplierId"
                  />
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
          
          <v-card class="mt-4">
            <v-card-title class="d-flex justify-space-between align-center">
              <span>报价明细</span>
              <v-btn
                prepend-icon="mdi-plus"
                color="primary"
                @click="addItem"
              >
                添加明细项
              </v-btn>
            </v-card-title>
            <v-card-text>
              <QuotationItem
                v-for="(item, index) in formData.details"
                :key="index"
                :item="item"
                :index="index"
                @update:item="updateItem(index, $event)"
                @remove="removeItem(index)"
              />
              
              <div v-if="formData.details.length === 0" class="text-center py-8">
                <v-icon size="64" color="grey-lighten-1">mdi-file-document-outline</v-icon>
                <p class="text-grey mt-2">暂无明细项，请添加</p>
              </div>
            </v-card-text>
          </v-card>
          
          <v-card class="mt-4">
            <v-card-title>总计</v-card-title>
            <v-card-text>
              <h2 class="text-h4 primary--text">
                ¥{{ totalAmount.toFixed(2) }}
              </h2>
            </v-card-text>
          </v-card>
          
          <div class="d-flex justify-end mt-6">
            <v-btn
              type="submit"
              color="primary"
              size="large"
              :loading="loading"
              :disabled="!canSubmit"
            >
              创建并发送报价单
            </v-btn>
          </div>
        </v-form>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useApi } from '@/composables/useApi'
import SupplierSelector from '@/components/SupplierSelector.vue'
import QuotationItem from '@/components/QuotationItem.vue'

const router = useRouter()
const { post } = useApi()

const formData = ref({
  supplierId: null,
  details: []
})

const loading = ref(false)

// 添加明细项
const addItem = () => {
  formData.value.details.push({
    itemDescription: '',
    serviceType: 'air_freight',
    origin: '',
    destination: '',
    weightOrVolume: 0,
    unitPrice: 0,
    amount: 0
  })
}

// 更新明细项
const updateItem = (index, item) => {
  formData.value.details[index] = item
}

// 删除明细项
const removeItem = (index) => {
  formData.value.details.splice(index, 1)
}

// 计算总金额
const totalAmount = computed(() => {
  return formData.value.details.reduce((sum, item) => sum + (item.amount || 0), 0)
})

// 检查是否可以提交
const canSubmit = computed(() => {
  return formData.value.supplierId && 
         formData.value.details.length > 0 &&
         formData.value.details.every(item => 
           item.itemDescription && 
           item.origin && 
           item.destination && 
           item.weightOrVolume >= 0 && 
           item.unitPrice >= 0
         )
})

// 提交报价单
const submitQuotation = async () => {
  if (!canSubmit.value) return
  
  loading.value = true
  try {
    const response = await post('/api/quotations', {
      supplierId: formData.value.supplierId,
      details: formData.value.details
    })
    
    // 显示成功消息
    console.log('报价单创建成功:', response.data)
    router.push({ name: 'dashboard' })
  } catch (error) {
    console.error('创建报价单失败:', error)
    // 显示错误消息
  } finally {
    loading.value = false
  }
}

// 初始化第一个明细项
addItem()
</script>