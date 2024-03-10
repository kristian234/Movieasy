'use client'

import { Fragment, useEffect } from "react";
import AppContainer from "../components/app-container";
import { signOut, useSession } from "next-auth/react";

export default function Home() {
  const { data: session } = useSession();

  useEffect(() => {
    if (session?.error === "RefreshAccessTokenError") {
      console.log("BREOAKFKOAEKOEA" + session?.error)
      signOut(); // Force sign in to hopefully resolve error
    }
  }, [session]);

  return (
    <Fragment>
      <AppContainer />
    </Fragment>
  );
}
