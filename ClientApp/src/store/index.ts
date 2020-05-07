import Vue from "vue";
import Vuex from "vuex";
import { Tool } from "@/models/Tool";
import Axios from "axios";
import { getField, updateField } from "vuex-map-fields";
import i18n from "../i18n";
import router from '../router';
Vue.use(Vuex);

const date = new Date();
date.setDate(date.getDate() + 1);

export default new Vuex.Store({
  state: {
    defaultTool: {
      serialNumber: "",
      currentSerialNumber: "",
      label: "",
      imageUrl: "",
      nextInspectionDate: date.toISOString().substr(0, 10),
    },
    newTool: {
      serialNumber: "",
      currentSerialNumber: "",
      label: "",
      imageUrl: "",
      nextInspectionDate: date.toISOString().substr(0, 10),
    },
    currentTool: {
      serialNumber: "",
      currentSerialNumber: "",
      label: "",
      imageUrl: "",
      nextInspectionDate: date.toISOString().substr(0, 10),
    },
    tools: [] as Tool[],
    showError: false,
    errorMessage: "",
    version: "1.0.0",
  },
  getters: {
    newTool: (state) => {
      return state.newTool;
    },
    currentTool: (state) => {
      return state.currentTool;
    },
    tools:(state) => {
      return state.tools;
    },
    getField,
  },
  actions: {
    async getTools({ commit }) {
      await Axios.get<Tool[]>("api/Tools")
        .then((response) => {
          commit("SET_TOOLS", response.data);
        })
        .catch((error) => {
          this.state.showError = true;
          this.state.errorMessage = `${i18n.t("errorLoadingTools")} ${error}.`;
        });
    },
    async addTool({ commit }, payload) {
      await Axios.post("api/Tools", payload)
        .then(() => {
          commit("ADD_TOOL", payload);
          router.push("/");
        })
        .catch((error) => {
          this.state.showError = true;
          this.state.errorMessage = `${i18n.t("errorAddingTools")} ${error}.`;
        });
    },
    async getTool({ commit }, payload) {
      await Axios.get<Tool>(`/api/Tools/${payload}`)
        .then((response) => {
          commit("SET_TOOL", response.data);
        })
        .catch((error) => {
          this.state.showError = true;
          this.state.errorMessage = `${i18n.t("errorLoadingData")} ${error}.`;
        });
    },
    async editTool({ commit }, payload) {
      await Axios.put(`/api/Tools/${payload.currentSerialNumber}`, payload)
        .then(() => {
          router.push("/");
        })
        .catch((error) => {
          this.state.showError = true;
          this.state.errorMessage = `${i18n.t("errorEditingTools")} ${error}.`;
        });
    },
    async deleteTool({ commit }, payload) {
      await Axios.delete(`/api/Tools/${payload}`)
        .then(() => {
        })
        .catch((error) => {
          this.state.showError = true;
          this.state.errorMessage = `${i18n.t("errorDeletingTools")} ${error}.`;
        });
    },
  },
  mutations: {
    ADD_TOOL(state, payload) {
      state.tools.push(payload);
      Object.assign(state.newTool, state.defaultTool);
    },
    SET_TOOLS(state, payload) {
      state.tools = payload;
    },
    SET_TOOL(state, payload) {
      state.currentTool = payload;
      state.currentTool.nextInspectionDate = payload.nextInspectionDate.substr(
        0,
        10
      );
    },
    updateField,
  },
  modules: {},
});
