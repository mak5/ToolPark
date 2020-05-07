<template>
  <v-container>
    <h2>{{$t('edit')}}</h2>
    <v-row justify="center">
      <v-col cols="6">
        <v-alert v-if="outdated" dense text type="warning">
          {{ $t("inspectionDateOutdated") }}
        </v-alert>
        <ToolForm :tool="currentTool" v-on:submited="submit"></ToolForm>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapFields } from "vuex-map-fields";
import ToolForm from "@/components/ToolForm.vue";

export default {
  components: {
    ToolForm,
  },
  data() {
    return {};
  },
  computed: {
    ...mapFields(["currentTool"]),
    outdated() {
      return new Date(this.currentTool.nextInspectionDate) < new Date();
    },
  },
  beforeDestroy() {
    this.$store.state.showError = false;
  },
  methods: {
    async submit() {
      await this.$store.dispatch("editTool", this.currentTool);
    },
  },
  async created() {
    await this.$store.dispatch("getTool", this.$route.params.id);
  },
};
</script>
