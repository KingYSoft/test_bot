<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title class="text-h5">欢迎使用空运海运费用报价系统</v-card-title>
          <v-card-text>
            <p>这是一个基于Vue3 + Vuetify3 + .NET 8.0的完整解决方案，用于管理空运和海运费用报价。</p>
            
            <div class="d-flex flex-wrap gap-4 mt-6">
              <v-btn 
                color="primary" 
                size="large"
                prepend-icon="mdi-file-document-edit"
                to="/quotations"
              >
                创建报价单
              </v-btn>
              
              <v-btn 
                color="secondary" 
                size="large"
                prepend-icon="mdi-format-list-bulleted"
                to="/quotations"
              >
                查看报价单
              </v-btn>
              
              <v-btn 
                color="accent" 
                size="large"
                prepend-icon="mdi-account-group"
                to="/suppliers"
              >
                管理供应商
              </v-btn>
            </div>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row class="mt-6">
      <v-col cols="12" md="4">
        <v-card>
          <v-card-title>系统功能</v-card-title>
          <v-card-text>
            <ul>
              <li>供应商管理</li>
              <li>报价单创建与编辑</li>
              <li>邮件通知供应商</li>
              <li>在线确认/拒绝报价</li>
              <li>报价单状态跟踪</li>
            </ul>
          </v-card-text>
        </v-card>
      </v-col>
      
      <v-col cols="12" md="4">
        <v-card>
          <v-card-title>技术栈</v-card-title>
          <v-card-text>
            <ul>
              <li>Vue 3 + Composition API</li>
              <li>Vuetify 3 UI框架</li>
              <li>.NET 8.0后端API</li>
              <li>Entity Framework Core</li>
              <li>SQL Server数据库</li>
            </ul>
          </v-card-text>
        </v-card>
      </v-col>
      
      <v-col cols="12" md="4">
        <v-card>
          <v-card-title>快速开始</v-card-title>
          <v-card-text>
            <p>点击上方按钮开始创建您的第一个报价单，或浏览现有报价单和供应商信息。</p>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
</script>