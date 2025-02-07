import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, Image, TouchableOpacity, SafeAreaView, Platform, StatusBar } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { router } from 'expo-router';
import Header from '../common/Header';
import Footer from '../common/Footer';

const Profile = () => {
  const [studentInfo, setStudentInfo] = useState({
    studentId: '102210098',
    className: '21TCLC_DT1',
    faculty: 'Công nghệ thông tin',
    academicYear: '2021-2026'
  });

  const signOut = () => {
    router.replace('/(auth)/Login');
  };

  const handleFooterPress = (route: string) => {
    if (route === 'home') {
      router.push('/(tabs)/Home');
    }
  };

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        {/* Header */}
        <Header title="HỒ SƠ" />

        {/* Profile Section */}
        <View style={styles.profileSection}>
          <Image
            source={{ uri: 'https://i.imghippo.com/files/Da5684EM.jpg' }}
            style={styles.avatar}
          />
          <Text style={styles.name}>Hà Ngọc Hưng</Text>
        </View>

        {/* Info Section */}
        <View style={styles.blankPage}>
          <View style={styles.blankRow}>
            <Text style={styles.blankText}>Mã số sinh viên: </Text>
            <Text style={styles.blankInfo}>102210098</Text>
          </View>
          <View style={styles.blankRow}>
            <Text style={styles.blankText}>Lớp sinh hoạt: </Text>
            <Text style={styles.blankInfo}>21TCLC_DT1</Text>
          </View>
          <View style={styles.blankRow}>
            <Text style={styles.blankText}>Khoa: </Text>
            <Text style={styles.blankInfo}>Công nghệ thông tin</Text>
          </View>
          <View style={styles.blankRow}>
            <Text style={styles.blankText}>Niên khoá: </Text>
            <Text style={styles.blankInfo}>2021-2026</Text>
          </View>
        </View>

        {/* Sign Out Button */}
        <TouchableOpacity
          style={styles.signOutButton}
          onPress={() => signOut()}
        >
          <Text style={styles.signOutText}>Đăng xuất</Text>
        </TouchableOpacity>

        {/* Footer */}
        <Footer />
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
    paddingTop: Platform.OS === 'android' ? StatusBar.currentHeight : 0,
  },
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#0000cc',
    padding: 15,
    height: 70,
    zIndex: 1,
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
    paddingVertical: 20,
    alignItems: 'center',
    backgroundColor: 'white',
  },
  avatar: {
    width: 150,
    height: 150,
    borderRadius: 100,
    marginBottom: 10,
  },
  name: {
    fontSize: 25,
    fontWeight: 'bold',
  },
  blankRow: {
    flexDirection: 'row',
    alignItems: 'baseline',
    marginBottom: 3,
  },
  blankPage: {
    paddingVertical: 20,
    paddingHorizontal: 15,
    backgroundColor: 'white',
  },
  blankText: {
    fontSize: 18,
    color: '#000',
    paddingLeft: 50,
    lineHeight: 24,
    paddingBottom: 5,
  },
  blankInfo: {
    fontSize: 18,
    fontWeight: 'bold',
    color: '#000',
    lineHeight: 24,
  },
  signOutButton: {
    backgroundColor: '#ff0000',
    paddingVertical: 12,
    borderRadius: 5,
    marginHorizontal: 50,
    marginTop: 20,
    marginBottom: 40,
    alignItems: 'center',
  },
  signOutText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
});

export default Profile;
