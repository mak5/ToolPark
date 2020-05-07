<template>
  <v-form ref="form">
    <v-text-field
      v-model="tool.serialNumber"
      :error-messages="serialNumberErrors"
      :counter="30"
      :label="$t('serialNumber')"
      required
      @input="$v.tool.serialNumber.$touch()"
      @blur="$v.tool.serialNumber.$touch()"
    ></v-text-field>
    <v-text-field
      v-model="tool.label"
      :error-messages="labelErrors"
      :counter="50"
      :label="$t('label')"
      required
      @input="$v.tool.label.$touch()"
      @blur="$v.tool.label.$touch()"
    ></v-text-field>
    <img v-if="editImage && !hasImage" :src="tool.imageUrl" />
    <template>
      <image-uploader
        :debug="1"
        :maxWidth="512"
        :quality="0.7"
        :autoRotate="true"
        outputFormat="string"
        :preview="true"
        :className="['fileinput', { 'fileinput--loaded': hasImage }]"
        :capture="false"
        accept="image/*"
        doNotResize="['gif', 'svg']"
        @input="setImage"
      >
        <label id="upload-region" for="fileInput" slot="upload-label">
          <figure>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="32"
              height="32"
              viewBox="0 0 32 32"
            >
              <path
                class="path1"
                d="M9.5 19c0 3.59 2.91 6.5 6.5 6.5s6.5-2.91 6.5-6.5-2.91-6.5-6.5-6.5-6.5 2.91-6.5 6.5zM30 8h-7c-0.5-2-1-4-3-4h-8c-2 0-2.5 2-3 4h-7c-1.1 0-2 0.9-2 2v18c0 1.1 0.9 2 2 2h28c1.1 0 2-0.9 2-2v-18c0-1.1-0.9-2-2-2zM16 27.875c-4.902 0-8.875-3.973-8.875-8.875s3.973-8.875 8.875-8.875c4.902 0 8.875 3.973 8.875 8.875s-3.973 8.875-8.875 8.875zM30 14h-4v-2h4v2z"
              ></path>
            </svg>
          </figure>
          <span class="upload-caption">{{
            hasImage || editImage ? $t("replace") : $t("clickToUpload")
          }}</span>
        </label>
      </image-uploader>
    </template>

    <v-menu
      v-model="menu2"
      :close-on-content-click="false"
      :nudge-right="40"
      transition="scale-transition"
      offset-y
      min-width="290px"
    >
      <template v-slot:activator="{ on }">
        <v-text-field
          v-model="tool.nextInspectionDate"
          :label="$t('nextInspectionDate')"
          prepend-icon="event"
          readonly
          v-on="on"
        ></v-text-field>
      </template>
      <v-date-picker
        v-model="tool.nextInspectionDate"
        :min="new Date().toISOString().substr(0, 10)"
        @input="menu2 = false"
      ></v-date-picker>
    </v-menu>
    <v-btn class="mr-4 mt-4" color="success" @click="submit">{{
      $t("submit")
    }}</v-btn>
    <v-btn class="mt-4" @click="clear">{{ $t("clear") }}</v-btn>
  </v-form>
</template>

<script>
import { Tool } from "@/models/Tool";
import { mapGetters, mapActions } from "vuex";
import ImageUploader from "vue-image-upload-resize";
import axios from "axios";
import { mapFields } from "vuex-map-fields";
import { validationMixin } from "vuelidate";
import { required, maxLength, minLength } from "vuelidate/lib/validators";
import ToolForm from "@/components/ToolForm.vue";

export default {
  name: "ToolForm",
  components: {
    ImageUploader,
  },
  mixins: [validationMixin],

  validations: {
    tool: {
      serialNumber: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(30),
        async isUnique(serialNumber) {
          if (
            serialNumber === "" ||
            serialNumber === this.tool.currentSerialNumber
          )
            return true;
          const response = await axios.get(
            `/api/Tools/IsUnique/${serialNumber}`
          );
          return Boolean(await response.data);
        },
      },
      label: { required, minLength: minLength(6), maxLength: maxLength(50) },
    },
  },
  data() {
    return {
      menu2: false,
      hasImage: false,
      image: null,
    };
  },
  props: ["tool"],
  computed: {
    editImage() {
      return this.tool.imageUrl && this.tool.imageUrl.length > 0;
    },
    serialNumberErrors() {
      const errors = [];
      if (!this.$v.tool.serialNumber.$dirty) return errors;
      !this.$v.tool.serialNumber.maxLength &&
        errors.push(this.$t("serialNumberMax"));
      !this.$v.tool.serialNumber.minLength && errors.push("serialNumberMin");
      !this.$v.tool.serialNumber.isUnique && errors.push("serialNumberUnique");
      !this.$v.tool.serialNumber.required &&
        errors.push("serialNumberRequired");
      return errors;
    },
    labelErrors() {
      const errors = [];
      if (!this.$v.tool.label.$dirty) return errors;
      !this.$v.tool.label.maxLength && errors.push("LabelMax");
      !this.$v.tool.label.minLength && errors.push("LabelMin");
      !this.$v.tool.label.required && errors.push("LabelRequired");
      return errors;
    },
  },
  methods: {
    submit() {
      this.$v.$touch();
      if (this.$v.$error) return;
      if (this.hasImage) this.tool.imageUrl = this.image;
      this.$emit("submited");
    },
    clear() {
      this.$refs.form.reset();
    },
    setImage(output) {
      this.hasImage = true;
      this.image = output;
    },
  },
};
</script>

<style>
#fileInput {
  display: none;
}
#upload-region {
  background: #ebedf1;
  display: block;
  width: 159px;
  padding: 20px;
  text-align: center;
  margin: 30px 0;
}
</style>
