import { Metadata } from "next";
import "../globals.css";
import Provider from "../Providers/Provider";
import RefreshClientComponent from "../components/shared/refresh-component";
import Header from "../layout/header";
import 'react-multi-carousel/lib/styles.css';
import ToasterProvider from "../Providers/ToasterProvider";
import ModalWrapper from "../Providers/ModalProvider";

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