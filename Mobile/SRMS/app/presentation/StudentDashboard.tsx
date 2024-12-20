import React, { useState } from 'react';
import { View, Text, TextInput, Image, StyleSheet, TouchableOpacity, TouchableWithoutFeedback } from 'react-native';
import { MaterialIcons } from '@expo/vector-icons'

const StudentDashboard = () => {
  const [sidebarVisible, setSidebarVisible] = useState(false);

  const handleOutsideClick = () => {
    if (sidebarVisible) {
      setSidebarVisible(false);
    }
  };

  return (
    <TouchableWithoutFeedback onPress={handleOutsideClick}>
      <View style={styles.container}>
        {/* Toggle Button */}
        <TouchableOpacity
          style={styles.toggleButton}
          onPress={(e) => {
            e.stopPropagation();
            setSidebarVisible(!sidebarVisible);
          }}>
          <Text style={styles.toggleButtonText}>{sidebarVisible ? '...' : '...'}</Text>
        </TouchableOpacity>

        {/* Sidebar */}
        {sidebarVisible && (
          <TouchableWithoutFeedback onPress={(e) => e.stopPropagation()}>
            <View style={styles.sidebar}>
              <Image
                source={{ uri: 'https://i.imghippo.com/files/mUo4100yA.webp' }}
                style={styles.logo}
              />
              <Text style={styles.title}>DUT SRMS</Text>
              <View style={styles.sidebarList}>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Khoa</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Giảng viên</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Sinh viên</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Chương trình đào tạo</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Học phần</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Lớp học phần</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Kết quả học tập</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Thống kê</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Hồ sơ cá nhân</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Cài đặt</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.sidebarItem}>
                  <Text style={styles.sidebarText}>Đăng xuất</Text>
                </TouchableOpacity>
              </View>
            </View>
          </TouchableWithoutFeedback>
        )}

        {/* Content */}
        <View style={[styles.content, sidebarVisible && { marginLeft: 250 }]}>
          <View style={styles.header}>
            <TextInput placeholder="Search" style={styles.searchBar} />
            <View style={styles.userInfo}>
              <Text style={styles.userName}>Đ. Thiên Bình</Text>
              <Text style={styles.userRole}>Admin</Text>
            </View>
          </View>

          {/* Table Section */}
          <View style={styles.tableContainer}>
            <View style={styles.tableHeader}>
              <Text style={styles.tableHeaderText}>Danh sách sinh viên</Text>
              <TextInput placeholder="Tìm kiếm..." style={styles.searchInput} />
            </View>

            {/* Table */}
            <View style={styles.tableRow}>
              <Text style={styles.tableCell}>STT</Text>
              <Text style={styles.tableCell}>MSSV</Text>
              <Text style={styles.tableCell}>Họ và tên</Text>
              <Text style={styles.tableCell}>Khoa</Text>
              <Text style={styles.tableCell}>Lớp</Text>
              <Text style={styles.tableCell}>SDT</Text>
              <Text style={styles.tableCell}>Nhập thiếu mục thông tin</Text>
              <Text style={styles.tableCell}>Hành động</Text>
            </View>

            {/* Data Row */}
            <View style={styles.tableRow}>
              <Text style={styles.tableCell}>1</Text>
              <Text style={styles.tableCell}>102230001</Text>
              <Text style={styles.tableCell}>Nguyễn Văn A</Text>
              <Text style={styles.tableCell}>K.Công nghệ thông tin</Text>
              <Text style={styles.tableCell}>23T_Nhat1</Text>
              <Text style={styles.tableCell}>0785243984</Text>
              <View style={styles.statusIcon}>
                <View style={styles.status}></View>
              </View>
              <View style={styles.actions}>
                <TouchableOpacity style={styles.action}>
                  <Text>Xem thông tin</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.action}>
                  <Text>Cài lại mật khẩu</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.action}>
                  <Text>Khóa tài khoản</Text>
                </TouchableOpacity>
                <TouchableOpacity style={styles.action}>
                  <Text>Xóa sinh viên này</Text>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>
      </View>
    </TouchableWithoutFeedback>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#e0f7fa',
    flexDirection: 'row',
  },
  toggleButton: {
    position: 'absolute',
    top: 20,
    left: 20,
    zIndex: 10,
    backgroundColor: '#007bff',
    padding: 10,
    borderRadius: 5,
  },
  toggleButtonText: {
    color: '#fff',
    fontWeight: '600',
  },
  sidebar: {
    width: 250,
    backgroundColor: '#fff',
    padding: 20,
    borderRightWidth: 1,
    borderColor: '#ddd',
  },
  content: {
    flex: 1,
    padding: 20,
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 20,
  },
  searchBar: {
    borderColor: '#ddd',
    borderWidth: 1,
    padding: 5,
    borderRadius: 5,
    flex: 1,
  },
  userInfo: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  userName: {
    fontWeight: '500',
    marginRight: 10,
  },
  userRole: {
    fontWeight: '300',
  },
  searchInput: {
    borderColor: '#ddd',
    borderWidth: 1,
    padding: 5,
    borderRadius: 5,
    marginBottom: 20,
  },
  tableContainer: {
    backgroundColor: '#fff',
    borderRadius: 8,
    padding: 15,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 10,
  },
  tableHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    borderBottomWidth: 1,
    borderColor: '#ddd',
    paddingBottom: 10,
  },
  tableHeaderText: {
    fontWeight: '700',
    fontSize: 16,
  },
  tableRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingVertical: 10,
  },
  tableCell: {
    fontSize: 14,
    flex: 1,
  },
  statusIcon: {
    justifyContent: 'center',
    alignItems: 'center',
  },
  status: {
    width: 20,
    height: 20,
    backgroundColor: '#4caf50',
    borderRadius: 10,
  },
  actions: {
    flexDirection: 'column',
    justifyContent: 'center',
  },
  action: {
    fontSize: 14,
    color: '#007bff',
    marginVertical: 5,
  },
  sidebarItem: {
    padding: 10,
    marginVertical: 5,
    borderRadius: 5,
  },
  sidebarText: {
    fontSize: 14,
    color: '#333',
  },
  sidebarList: {
    marginTop: 20,
  },
  title: {
    fontSize: 20,
    fontWeight: 'bold',
    marginVertical: 15,
    textAlign: 'center',
  },
  logo: {
    width: 100,
    height: 100,
    alignSelf: 'center',
    marginBottom: 10,
  },
});

export default StudentDashboard;
