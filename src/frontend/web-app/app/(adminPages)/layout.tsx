import { Metadata } from "next";
import "../globals.css";
import Provider from "../Providers/Provider";
import RefreshClientComponent from "../components/shared/refresh-component";
import AdminHeader from "../components/admin/admin-header/header";
import { ProSidebarProvider } from "react-pro-sidebar";
import Head from "next/head";
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
              <div className="flex">
                <AdminHeader />
                <main className="flex-grow">
                  <ToasterProvider />
                  <ModalWrapper>
                    {children}
                  </ModalWrapper>
                </main>
              </div>
            </RefreshClientComponent>
          </Provider>
        </div>
      </body>
    </html >
  )
}
