<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card class="pa-4">
          <v-card-title class="headline">
            {{ title }}
          </v-card-title>
          
          <v-card-text>
            <p>{{ description }}</p>
            
            <v-divider class="my-4"></v-divider>
            
            <v-list>
              <v-list-item v-for="(item, index) in features" :key="index">
                <template v-slot:prepend>
                  <v-icon color="primary">mdi-check-circle</v-icon>
                </template>
                <v-list-item-title>{{ item }}</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-card-text>
          
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" @click="onClick">
              {{ buttonText }}
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
interface Props {
  title: string;
  description: string;
  features: string[];
  buttonText: string;
}

interface Emits {
  (e: 'click'): void;
}

defineProps<Props>();
const emit = defineEmits<Emits>();

const onClick = () => {
  emit('click');
};
</script>