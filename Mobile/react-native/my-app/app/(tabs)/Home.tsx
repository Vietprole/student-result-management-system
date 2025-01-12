import React from 'react';
import { View, TouchableOpacity, StyleSheet, SafeAreaView, StatusBar, Image, Text, Platform } from 'react-native';
import Ionicons from '@expo/vector-icons/Ionicons';
import { router } from 'expo-router';

const Dashboard = () => {
  const handleFooterPress = (route: string) => {
    if (route === 'person') {
      router.push('/(tabs)/Profile');
    }
  };

  const handleGridPress = (icon: string) => {
    switch (icon) {
      case 'school-outline':
        router.push('/(tabs)/GiangVien');
        break;
      case 'briefcase-outline':
        router.push('/(tabs)/Teaching_Program');
        break;
      case 'folder-open-outline':
        router.push('/(tabs)/HocPhan');
        break;
      case 'library-outline':
        router.push('/(tabs)/LopHocPhan');
        break;
      case 'document-text-outline':
        router.push('/(tabs)/Result');
        break;
      case 'list-outline':
        router.push('/(tabs)/ThongKePLO');
        break;
    }
  };

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        <View style={styles.header}>
          <View style={styles.headerLeft}>
            {/* source={require('../../assets/images/logo-DUT.png')} */}
            <Image
              source={{ uri: 'https://i.imghippo.com/files/mUo4100yA.webp' }}
              style={styles.headerLogo}
            />
          </View>
          <Text style={styles.logoText}>DUT SRMS</Text>
          <Ionicons name="notifications-outline" size={24} color="white" />
        </View>

        {/* source={require('../../assets/images/profile.png')} */}
        <View style={styles.profileSection}>
          <Image
            source={{ uri: 'https://i.imghippo.com/files/Da5684EM.jpg' }}
            style={styles.avatar}
          />
          <View>
            <Text>
              <Text style={styles.name}>Hà Ngọc Hưng</Text>
              <Text style={styles.id}> | 102210098</Text>
            </Text>
            <Text style={styles.details}>Khoa CNTT</Text>
          </View>
        </View>

        <View style={styles.grid}>
          {gridItems.map((item, index) => (
            <TouchableOpacity
              key={index}
              style={styles.card}
              onPress={() => handleGridPress(item.icon)}
            >
              <Ionicons name={item.icon} size={32} color="#0000cc" />
              <Text style={styles.cardText}>{item.label}</Text>
            </TouchableOpacity>
          ))}
        </View>

        <View style={styles.footer}>
          {footerItems.map((item, index) => (
            <TouchableOpacity
              key={index}
              style={styles.footerItem}
              onPress={() => handleFooterPress(item.icon)}
            >
              <Ionicons name={item.icon} size={24} color="#0000cc" />
              <Text style={styles.footerText}>{item.label}</Text>
            </TouchableOpacity>
          ))}
        </View>
      </View>
    </SafeAreaView>
  );
};

type IconName = 'school-outline' | 'briefcase-outline' | 'folder-open-outline' | 'library-outline' | 'document-text-outline' | 'list-outline' | 'home' | 'person';

const gridItems: { icon: IconName, label: string }[] = [
  { icon: 'school-outline', label: 'Giảng viên' },
  { icon: 'briefcase-outline', label: 'Chương trình đào tạo' },
  { icon: 'folder-open-outline', label: 'Học phần' },
  { icon: 'library-outline', label: 'Lớp học phần' },
  { icon: 'document-text-outline', label: 'Kết quả học tập' },
  { icon: 'list-outline', label: 'Thống kê PLO' },
];

const footerItems: { icon: IconName, label: string }[] = [
  { icon: 'home', label: 'Trang chủ' },
  { icon: 'person', label: 'Hồ sơ' },
];

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
    paddingTop: Platform.OS === 'android' ? StatusBar.currentHeight : 0,
  },
  container: {
    flex: 1,
    backgroundColor: '#e0f7fa',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#0000cc',
    padding: 15,
    height: 70,
  },
  headerLeft: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  headerLogo: {
    width: 50,
    height: 50,
    marginRight: 10,
  },
  logoText: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  profileSection: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 15,
    backgroundColor: 'white',
  },
  avatar: {
    width: 50,
    height: 50,
    borderRadius: 25,
    marginRight: 15,
  },
  name: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  id: {
    fontSize: 16,
    fontWeight: 'normal',
  },
  details: {
    fontSize: 14,
    color: '#555',
  },
  grid: {
    flex: 1,
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'space-around',
    marginTop: 10
  },
  card: {
    width: '39%',
    backgroundColor: 'white',
    borderRadius: 10,
    padding: 15,
    marginBottom: 15,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 3,
    elevation: 3,
  },
  cardText: {
    marginTop: 10,
    fontSize: 14,
    fontWeight: 'bold',
    textAlign: 'center',
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: 'white',
    paddingVertical: 10,
    borderTopWidth: 1,
    borderTopColor: '#ddd',
  },
  footerItem: {
    alignItems: 'center',
  },
  footerText: {
    marginTop: 5,
    fontSize: 12,
    color: '#0000cc',
  },
});

export default Dashboard;