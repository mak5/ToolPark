import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'tools',
      component: () => import('./views/ToolsList.vue'),
    },
    {
      path: '/addtool',
      name: 'addtool',
      component: () => import('./views/AddTool.vue'),
    },
    {
      path: '/edittool/:id',
      name: 'edittool',
      component: () => import('./views/EditTool.vue'),
    },
  ],
});
