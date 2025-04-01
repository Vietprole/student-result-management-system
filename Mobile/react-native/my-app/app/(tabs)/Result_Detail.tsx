import React from 'react';
import { View, Text, StyleSheet, SafeAreaView, TouchableOpacity, StatusBar, Platform } from 'react-native';
import { useLocalSearchParams, router } from 'expo-router';
import { Ionicons } from '@expo/vector-icons';
import { apiClient } from '../api/apiClient';

const Result_Detail = () => {
  const params = useLocalSearchParams();

  return (
    <SafeAreaView style={styles.safeArea}>
      <StatusBar barStyle="light-content" backgroundColor="#0000cc" />
      <View style={styles.container}>
        {/* Header */}
        <View style={styles.header}>
          <TouchableOpacity onPress={() => router.push('/(tabs)/Result')} style={styles.backButton}>
            <Ionicons name="arrow-back" size={24} color="white" />
          </TouchableOpacity>
          <Text style={styles.headerTitle}>CHI TIẾT ĐIỂM</Text>
          <View style={styles.headerRight} />
        </View>

        {/* Content */}
        <View style={styles.content}>
          <Text style={styles.subjectName}>{params.monHoc}</Text>
          <View style={styles.scoreContainer}>
            <ScoreItem label="Mã lớp học phần" value={params.MaLHP as string} />
            <ScoreItem label="Số tín chỉ" value={params.SoTC as string} />
            <ScoreItem label="Công thức điểm" value={params.CTDiem as string} />
            <ScoreItem label="Bài tập" value={params.bt as string} />
            <ScoreItem label="Cuối kỳ" value={params.ck as string} />
            <ScoreItem label="Giữa kỳ" value={params.gk as string} />
            <ScoreItem label="Quá trình" value={params.qt as string} />
            <ScoreItem label="Tổng kết" value={params.tongKet as string} />
            <ScoreItem label="Thang 10" value={params.thang10 as string} />
            <ScoreItem label="Thang 4" value={params.thang4 as string} />

          </View>
        </View>
      </View>
    </SafeAreaView>
  );
};

const ScoreItem = ({ label, value }: { label: string, value: string }) => (
  <View style={styles.scoreItem}>
    <Text style={styles.scoreLabel}>{label}</Text>
    <Text style={styles.scoreValue}>{value || '-'}</Text>
  </View>
);

const styles = StyleSheet.create({
  safeArea: {
    flex: 1,
    backgroundColor: '#000',
  },
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: '#0000cc',
    padding: 15,
    height: 70,
  },
  backButton: {
    padding: 8,
  },
  headerTitle: {
    color: 'white',
    fontSize: 20,
    fontWeight: 'bold',
  },
  headerRight: {
    width: 40,
  },
  content: {
    padding: 20,
  },
  subjectName: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    color: '#0000cc',
  },
  scoreContainer: {
    backgroundColor: '#f5f5f5',
    borderRadius: 10,
    padding: 15,
  },
  scoreItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingVertical: 12,
    borderBottomWidth: 1,
    borderBottomColor: '#ddd',
  },
  scoreLabel: {
    fontSize: 16,
    color: '#333',
  },
  scoreValue: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#0000cc',
  },
});

export default Result_Detail;