<template>
  <v-app dark>
    <v-app-bar app dense>
      <v-app-bar-nav-icon></v-app-bar-nav-icon>

      <v-toolbar-title>Tricking Library</v-toolbar-title>

      <v-spacer></v-spacer>


      <v-menu left bottom>
        <template v-slot:activator="{ on, attrs }">

        </template>

        <v-list>
          <v-list-item @click="() => {}">
            <v-list-item-title>Trick</v-list-item-title>
          </v-list-item>
          <v-list-item @click="() => {}">
            <v-list-item-title>Submission</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
      <video-upload/>
    </v-app-bar>

    <v-main>
      <nuxt/>
    </v-main>
  </v-app>
</template>

<script>
import VideoUpload from "../components/video-upload"


export default {
components:{VideoUpload}
}

</script>
