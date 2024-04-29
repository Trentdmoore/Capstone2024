import { createRouter, createWebHashHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import('../View/MainView.vue')
  },
  {
    path: '/Create',
    name: 'create',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../View/CreateView.vue')
  },
  {
    path: '/LogInfo/:id',
    name: 'logs',
    component: () => import('../View/LogsView.vue')

  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
