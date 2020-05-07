<template>
  <v-container fluid>
    <v-row justify="end">
      <v-btn class="ma-2" to="/addtool" color="info"
        ><v-icon>mdi-plus</v-icon>{{ $t("addNewTool") }}</v-btn
      >
    </v-row>

    <v-slide-y-transition mode="out-in">
      <v-row>
        <v-col>
          <v-data-table
            :headers="headers"
            :search="search"
            :items="tools"
            :items-per-page="5"
            :loading="loading"
            class="elevation-1"
          >
            <template v-slot:top>
              <v-toolbar flat color="white">
                <v-toolbar-title>{{ $t("toolsList") }}</v-toolbar-title>
                <v-divider class="mx-4" inset vertical></v-divider>

                <v-spacer></v-spacer>

                <v-text-field
                  v-model="search"
                  append-icon="mdi-magnify"
                  :label="$t('search')"
                  single-line
                  hide-details
                ></v-text-field>
              </v-toolbar>
            </template>
            <v-progress-linear
              v-slot:progress
              color="blue"
              indeterminate
            ></v-progress-linear>
            <template v-slot:item.nextInspectionDate="{ item }">
              <td>{{ item.nextInspectionDate | date }}</td>
            </template>
            <template v-slot:item.actions="{ item }">
              <v-icon small class="mr-2" @click="editItem(item)">
                mdi-pencil
              </v-icon>
              <v-icon small @click="deleteItem(item)">
                mdi-delete
              </v-icon>
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </v-slide-y-transition>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import { Tool } from "../models/Tool";
import axios from "axios";
import router from "../router";
import { mapGetters } from "vuex";

export default Vue.extend({
  data() {
    return {
      loading: true,
      search: "",
      headers: [
        { text: this.$t("label"), value: "label" },
        { text: this.$t("serialNumber"), value: "serialNumber" },
        { text: this.$t("nextInspectionDate"), value: "nextInspectionDate" },
        { text: this.$t("actions"), value: "actions", sortable: false },
      ],
    };
  },
  computed: {
    ...mapGetters(["tools"]),
  },
  beforeDestroy() {
    this.$store.state.showError = false;
  },
  methods: {
    async fetchTools() {
      await this.$store.dispatch("getTools");
      this.loading = false;
    },
    editItem(item) {
      this.$router.push({ path: `/edittool/${item.serialNumber}` });
    },
    async deleteItem(item) {
      if (confirm(this.$tc("confirmMessage"))) {
        await this.$store.dispatch("deleteTool", item.serialNumber);
        await this.fetchTools();
      }
    },
  },
  async created() {
    await this.fetchTools();
  },
});
</script>
