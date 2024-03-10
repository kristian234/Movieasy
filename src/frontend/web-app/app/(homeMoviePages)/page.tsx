import { Fragment, useEffect } from "react";
import AppContainer from "../components/app-container";
import { signIn, signOut, useSession } from "next-auth/react";

export default function Home() {
  
  return (
    <Fragment>
      <AppContainer />
    </Fragment>
  );
}
