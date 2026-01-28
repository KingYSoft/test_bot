<template>
  <v-card class="mb-4">
    <v-card-title class="d-flex justify-space-between align-center">
      <span>明细项 {{ index + 1 }}</span>
      <v-btn
        v-if="index > 0"
        icon="mdi-delete"
        variant="text"
        color="error"
        @click="$emit('remove')"
      ></v-btn>
    </v-card-title>
    
    <v-card-text>
      <v-row>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="item.itemDescription"
            label="服务描述"
            variant="outlined"
            @update:model-value="updateItem"
          ></v-text-field>
        </v-col>
        
        <v-col cols="12" md="6">
          <v-select
            v-model="item.serviceType"
            :items="serviceTypes"
            label="服务类型"
            variant="outlined"
            @update:model-value="updateItem"
          ></v-select>
        </v-col>
        
        <v-col cols="12" md="6">
          <v-text-field
            v-model="item.origin"
            label="起始地"
            variant="outlined"
            @update:model-value="updateItem"
          ></v-text-field>
        </v-col>
        
        <v-col cols="12" md="6">
          <v-text-field
            v-model="item.destination"
            label="目的地"
            variant="outlined"
            @update:model-value="updateItem"
          ></v-text-field>
        </v-col>
        
        <v-col cols="12" md="4">
          <v-text-field
            v-model.number="item.weightOrVolume"
            label="重量/体积"
            type="number"
            step="0.01"
            variant="outlined"
            @update:model-value="calculateAmount"
          ></v-text-field>
        </v-col>
        
        <v-col cols="12" md="4">
          <v-text-field
            v-model.number="item.unitPrice"
            label="单价"
            type="number"
            step="0.01"
            variant="outlined"
            @update:model-value="calculateAmount"
          ></v-text-field>
        </v-col>
        
        <v-col cols="12" md="4">
          <v-text-field
            v-model.number="item.amount"
            label="金额"
            type="number"
            step="0.01"
            variant="outlined"
            readonly
          ></v-text-field>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>
</template>

<script setup>
import { ref, watch } from 'vue'

const emit = defineEmits(['update:item', 'remove'])

const props = defineProps({
  item: Object,
  index: Number
})

const serviceTypes = [
  { title: '空运', value: 'air_freight' },
  { title: '海运', value: 'sea_freight' },
  { title: '陆运', value: 'land_transport' },
  { title: '清关', value: 'customs_clearance' },
  { title: '保险', value: 'insurance' }
]

const calculateAmount = () => {
  if (props.item.weightOrVolume && props.item.unitPrice) {
    props.item.amount = parseFloat((props.item.weightOrVolume * props.item.unitPrice).toFixed(2))
    updateItem()
  }
}

const updateItem = () => {
  emit('update:item', props.item)
}

// 监听数值变化自动计算金额
watch(() => [props.item.weightOrVolume, props.item.unitPrice], () => {
  calculateAmount()
})
</script>