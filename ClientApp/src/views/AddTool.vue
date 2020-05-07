<template>
  <v-container>
    <h2>{{ $t("addNewTool") }}</h2>
    <v-row justify="center">
      <v-col cols="6">
        <ToolForm :tool="newTool" v-on:submited="submit"></ToolForm>
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
    ...mapFields(["newTool"]),
  },
  beforeDestroy() {
    this.$store.state.showError = false;
  },
  methods: {
    async submit() {
      await this.$store.dispatch("addTool", this.newTool);
    },
  },
};
</script>
