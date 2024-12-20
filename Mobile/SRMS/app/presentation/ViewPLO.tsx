import React from "react";
import { View, Text, StyleSheet, TextInput, FlatList, TouchableOpacity, Image } from "react-native";

// Define the PLO type
interface PLO {
  id: string;
  name: string;
}

const Dashboard = () => {
  const PLOs: PLO[] = [
    { id: "1", name: "PLO 1" },
    { id: "2", name: "PLO 2" },
    { id: "3", name: "PLO 3" },
    { id: "4", name: "PLO 4" },
    { id: "5", name: "PLO 5" },
    { id: "6", name: "PLO 6" },
    { id: "7", name: "PLO 7" },
    { id: "8", name: "PLO 8" },
  ];

  const renderPLO = ({ item }: { item: PLO }) => (
    <View style={styles.row}>
      <Text style={styles.cell}>{item.id}</Text>
      <Text style={styles.cell}>{item.name}</Text>
    </View>
  );

  return (
    <View style={styles.container}>
      {/* Sidebar */}
      <View style={styles.sidebar}>
        <Image
          source={{ uri: "https://i.imghippo.com/files/mUo4100yA.webp" }}
          style={styles.logo}
        />
        <Text style={styles.title}>DUT SRMS</Text>
        <View style={styles.menu}>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Khoa</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Giảng viên</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Sinh viên</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Chương trình đào tạo</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Học phần</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Lớp học phần</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Kết quả học tập</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Thống kê</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={[styles.menuItem, styles.activeMenuItem]}>Cài đặt</Text>
          </TouchableOpacity>
          <TouchableOpacity>
            <Text style={styles.menuItem}>Đăng xuất</Text>
          </TouchableOpacity>
        </View>
      </View>

      {/* Content */}
      <View style={styles.content}>
        <View style={styles.header}>
          <Text style={styles.headerText}>PLO - Nhập PLO</Text>
          <TextInput
            placeholder="Tìm kiếm..."
            style={styles.searchInput}
            accessibilityLabel="Search input"
          />
        </View>

        {/* Table */}
        <FlatList<PLO>
          data={PLOs}
          keyExtractor={(item) => item.id}
          renderItem={renderPLO}
          ListHeaderComponent={
            <View style={styles.row}>
              <Text style={[styles.cell, styles.headerCell]}>TT</Text>
              <Text style={[styles.cell, styles.headerCell]}>PLOs</Text>
            </View>
          }
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: "row",
    backgroundColor: "#f5f5f5",
  },
  sidebar: {
    width: 250,
    backgroundColor: "#fff",
    borderRightWidth: 1,
    borderColor: "#ddd",
    padding: 20,
  },
  logo: {
    width: 80,
    height: 80,
    alignSelf: "center",
  },
  title: {
    fontSize: 20,
    textAlign: "center",
    marginVertical: 10,
  },
  menu: {
    marginTop: 20,
  },
  menuItem: {
    paddingVertical: 10,
    fontSize: 16,
  },
  activeMenuItem: {
    backgroundColor: "#d9f1ff",
    borderRadius: 5,
    paddingLeft: 5,
  },
  content: {
    flex: 1,
    padding: 20,
  },
  header: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 20,
  },
  headerText: {
    fontSize: 18,
    fontWeight: "bold",
  },
  searchInput: {
    borderWidth: 1,
    borderColor: "#ddd",
    borderRadius: 5,
    padding: 5,
    width: "40%",
  },
  tableContainer: {
    backgroundColor: "#fff",
    borderRadius: 8,
    padding: 10,
    boxShadow: "0 2px 5px rgba(0, 0, 0, 0.1)",
  },
  row: {
    flexDirection: "row",
    borderBottomWidth: 1,
    borderColor: "#ddd",
  },
  cell: {
    flex: 1,
    padding: 10,
  },
  headerCell: {
    backgroundColor: "#e5f3ff",
    fontWeight: "bold",
  },
});

export default Dashboard;
