<template>
  <v-autocomplete
    v-model="selectedSupplier"
    :items="suppliers"
    item-title="name"
    item-value="id"
    label="选择供应商"
    variant="outlined"
    :loading="loading"
    clearable
    @update:model-value="$emit('update:selectedSupplier', $event)"
  >
    <template #item="{ props, item }">
      <v-list-item v-bind="props">
        <v-list-item-subtitle>{{ item.raw.email }}</v-list-item-subtitle>
      </v-list-item>
    </template>
  </v-autocomplete>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useApi } from '@/composables/useApi'

const { get } = useApi()
const emit = defineEmits(['update:selectedSupplier'])

const props = defineProps({
  modelValue: Number
})

const selectedSupplier = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:selectedSupplier', value)
})

const suppliers = ref([])
const loading = ref(false)

onMounted(async () => {
  loading.value = true
  try {
    const response = await get('/api/suppliers')
    suppliers.value = response.data
  } catch (error) {
    console.error('获取供应商失败:', error)
  } finally {
    loading.value = false
  }
})
</script>