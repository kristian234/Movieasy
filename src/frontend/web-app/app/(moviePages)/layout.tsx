import { Metadata } from "next";
import "../globals.css";
import Provider from "../Providers/Provider";
import RefreshClientComponent from "../components/shared/refresh-component";
import Header from "../layout/header";
import 'react-multi-carousel/lib/styles.css';
import ToasterProvider from "../Providers/ToasterProvider";
import ModalWrapper from "../Providers/ModalProvider";
import Head from "next/head";

export const metadata: Metadata = {
  title: "Movieasy",
  description: "Movieasy",
};

export default async function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en">
    	<Head>
          <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        </Head>
      <body>
        <div className="min-h-screen flex-auto max-h-screen overflow-auto">
          <Provider>
            <RefreshClientComponent >
              <Header />
              <main>
                <ToasterProvider />
                <ModalWrapper>
                  {children}
                </ModalWrapper>
              </main>
            </RefreshClientComponent>
          </Provider>
        </div>
      </body>
    </html>
  )
}