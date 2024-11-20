import { SidebarProvider, SidebarInset } from "@/components/sidebar"
import { AppSidebar } from "@/components/AppSidebar"
import PropTypes from 'prop-types';
import Header from "@/components/Header";

export default function Layout({ children }) {
  return (
    <SidebarProvider>
      <AppSidebar />
      <SidebarInset>
        <Header />
        {children}
      </SidebarInset>
    </SidebarProvider>
  )
}

Layout.propTypes = {
  children: PropTypes.node
};
